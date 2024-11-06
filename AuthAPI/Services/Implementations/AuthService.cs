using AuthAPI.Dtos;
using AuthAPI.Entities;
using AuthAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using AuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AuthAPI.Services.Implementations;

public class AuthService : IAuthService
{
	private readonly AppDbContext _authDbContext;
	private readonly IConfiguration _configuration;

	public AuthService(AppDbContext authDbContext, IConfiguration configuration)
	{
		_authDbContext = authDbContext;
		_configuration = configuration;
	}

	public async Task<GetUserResponseDto?> GetUserByIdAsync(int userId)
	{
		var user = await _authDbContext.Users
		.Include(u => u.Role)
		.Include(u => u.RefreshTokens)
		.SingleOrDefaultAsync(u => u.Id == userId);

		if (user == null)
			return null;

		return new GetUserResponseDto
		{
			Id = user.Id,
			Username = user.Username,
			Email = user.Email,
			RoleName = user.Role.Name,
			RoleId = user.RoleId,
			RefreshTokens = user.RefreshTokens.Select(x => x.Token).ToList()
		};
	}

	public async Task<List<GetUserResponseDto>> GetAllUsersAsync()
	{
		return await _authDbContext.Users
		.Include(u => u.Role)
		.Include(u => u.RefreshTokens)
		.Select(u => new GetUserResponseDto
		{
			Id = u.Id,
			Username = u.Username,
			Email = u.Email,
			RoleName = u.Role.Name,
			RoleId = u.RoleId,
			RefreshTokens = u.RefreshTokens.Select(x => x.Token).ToList()
		})
		.ToListAsync();
	}

	public async Task<TokenResponseDto?> UpdateUserAsync(UpdateUserDto updateDto)
	{
		var userToUpdate = await _authDbContext.Users
				.Include(u => u.Role)
				.SingleOrDefaultAsync(u => u.Email == updateDto.OldEmail);

		if (userToUpdate == null)
			return null;

		userToUpdate.Username = updateDto.Username;
		userToUpdate.Email = updateDto.Email;

		_authDbContext.Users.Update(userToUpdate);
		await _authDbContext.SaveChangesAsync();

		var newJwtToken = GenerateJwtToken(userToUpdate);
		var newRefreshToken = GenerateRefreshToken();

		var refreshTokenEntity = new RefreshToken
		{
			Token = newRefreshToken,
			UserId = userToUpdate.Id,
			ExpiryDate = DateTime.UtcNow.AddDays(7)
		};

		_authDbContext.RefreshTokens.Add(refreshTokenEntity);
		await _authDbContext.SaveChangesAsync();

		return new TokenResponseDto
		{
			AccessToken = newJwtToken,
			RefreshToken = newRefreshToken,
			Expiration = refreshTokenEntity.ExpiryDate
		};
	}

	public async Task<bool> DeleteUserAsync(int userId)
	{
		var user = await _authDbContext.Users
			.Include(u => u.Role)
			.Include(u => u.RefreshTokens)
			.SingleOrDefaultAsync(u => u.Id == userId);

		if (user == null) return false;

		_authDbContext.RefreshTokens.RemoveRange(user.RefreshTokens);
		_authDbContext.Users.Remove(user);
		await _authDbContext.SaveChangesAsync();

		return true;
	}

	public async Task<TokenResponseDto?> LoginAsync(LoginDto dto)
	{
		if (dto == null) return null;

		var loginPasswordHash = HashPassword(dto.Password);

		var user = await _authDbContext.Users
			.Include(u => u.Role)
			.Include(u => u.RefreshTokens)
			.SingleOrDefaultAsync(u => u.Email == dto.Email && u.PasswordHash == loginPasswordHash);

		if (user == null) return null;

		if (dto.Project == "admin" && user.Role.Name != "admin")
			return null;

		if (dto.Project == "portfoy" && user.Role.Name != "commenter")
			return null;

		_authDbContext.RefreshTokens.Where(u => u.UserId == user.Id)
			.ToList()
			.ForEach(t => t.IsRevoked = true);

		await _authDbContext.SaveChangesAsync();

		var jwt = GenerateJwtToken(user);
		var refreshToken = GenerateRefreshToken();

		var refreshTokenModel = new RefreshToken
		{
			Token = refreshToken,
			UserId = user.Id,
			ExpiryDate = DateTime.UtcNow.AddDays(7)
		};

		_authDbContext.RefreshTokens.Add(refreshTokenModel);
		await _authDbContext.SaveChangesAsync();

		return new TokenResponseDto
		{
			AccessToken = jwt,
			RefreshToken = refreshToken,
			Expiration = refreshTokenModel.ExpiryDate
		};
	}

	public async Task<bool> RegisterAsync(RegisterDto registerDto)
	{
		var userExists = await _authDbContext.Users.AnyAsync(u => u.Email == registerDto.Email);
		if (userExists) return false;

		var passwordHash = HashPassword(registerDto.Password);

		var user = new User()
		{
			Username = registerDto.Username,
			Email = registerDto.Email.ToLower(),
			PasswordHash = passwordHash,
			RoleId = registerDto.Project == "admin" ? 1 : 2
		};

		_authDbContext.Users.Add(user);
		await _authDbContext.SaveChangesAsync();

		return true;
	}

	public async Task<TokenResponseDto?> RefreshTokenAsync(string token)
	{
		var refreshToken = await _authDbContext.RefreshTokens
			.Include(x => x.User)
			.SingleOrDefaultAsync(x =>
				x.Token == token &&
				x.ExpiryDate > DateTime.UtcNow &&
				!x.IsRevoked &&
				!x.IsUsed);

		if (refreshToken == null) return null;

		refreshToken.IsUsed = true;

		var jwt = GenerateJwtToken(refreshToken.User);
		var newRefreshToken = GenerateRefreshToken();

		var refreshTokenEntity = new RefreshToken
		{
			Token = newRefreshToken,
			UserId = refreshToken.User.Id,
			ExpiryDate = DateTime.UtcNow.AddDays(7)
		};
		_authDbContext.RefreshTokens.Add(refreshTokenEntity);
		await _authDbContext.SaveChangesAsync();

		return new TokenResponseDto
		{
			AccessToken = jwt,
			RefreshToken = newRefreshToken
		};
	}

	public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
	{
		if (dto == null) return false;

		var user = await _authDbContext.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);

		if (user == null) return false;

		var resetPasswordToken = Guid.NewGuid().ToString("n");

		user.ResetPasswordToken = resetPasswordToken;
		_authDbContext.Users.Update(user);
		await _authDbContext.SaveChangesAsync();

		await SendResetPasswordEmailAsync(user);

		return true;
	}

	public async Task<bool> RenewPasswordAsync(RenewPasswordRequestDto dto)
	{
		if (dto == null || string.IsNullOrEmpty(dto.VerificationCode) || string.IsNullOrEmpty(dto.NewPassword))
			return false;

		var user = await _authDbContext.Users.SingleOrDefaultAsync(u => u.ResetPasswordToken == dto.VerificationCode);

		if (user == null) return false;

		var newPasswordHash = HashPassword(dto.NewPassword);
		user.PasswordHash = newPasswordHash;
		user.ResetPasswordToken = null;

		_authDbContext.Users.Update(user);
		await _authDbContext.SaveChangesAsync();

		return true;
	}

	private string GenerateJwtToken(User user)
	{
		var claims = new[]
		{
			new Claim(ClaimTypes.Sid, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Username),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, user.Role.Name)
		};

		var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

		var jwt = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireMinutes")),
			signingCredentials: signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(jwt);
	}

	private string GenerateRefreshToken()
	{
		return Guid.NewGuid().ToString();
	}

	private async Task SendResetPasswordEmailAsync(User user)
	{
		const string host = "smtp.gmail.com";
		const int port = 587;
		const string from = "denemebackend105@gmail.com";
		const string password = "boyc tvfg jkgp thpk";

		using SmtpClient client = new(host, port)
		{
			Credentials = new NetworkCredential(from, password),
			EnableSsl = true
		};

		MailMessage mail = new()
		{
			From = new MailAddress(from),
			Subject = "Şifre Sıfırlama",
			Body = $"Merhaba {user.Username}, <br> Şifrenizi sıfırlamak için <a href='https://localhost:7239/renew-password/verificationCode={user.ResetPasswordToken}'>tıklayınız</a>.",
			IsBodyHtml = true,
		};

		mail.To.Add(user.Email);

		await client.SendMailAsync(mail);
	}

	private static byte[] HashPassword(string password)
	{
		using SHA256 sha256 = SHA256.Create();
		return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
	}
}
