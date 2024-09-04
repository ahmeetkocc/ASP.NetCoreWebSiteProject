using AuthAPI.Data;
using AuthAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        //GetUser
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] int userId)
        {
            var user = _appDbContext.Users.Find(userId);

            if (user is null)
                return BadRequest("Kullanıcı bulunamadi");

            return Ok(user);
        }

        //GetUserList
        [HttpGet("UserList")]
        public IActionResult GetUsers()
        {
            var userList = _appDbContext.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .Select(u => new {
                    u.Id,
                    u.Username,
                    u.Email,
                    RoleName = u.Role.Name,
                    u.RoleId,
                    RefreshTokens = u.RefreshTokens.Select(x => x.Token)
                })
                .ToList();
            return Ok(userList);
        }

        //DeleteUser
        [HttpPost("delete/{userId}")]
        public IActionResult Delete([FromRoute] int userId)
        {
            var user = _appDbContext.Users
                .Include(u => u.Role)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault(u => u.Id == userId);

            if (user is null)
                return NoContent();

            //İlişkili yerlerin nasıl silineceği ile ilgili hata alındı.
            _appDbContext.RefreshTokens.RemoveRange(user.RefreshTokens);

            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();

            return Ok();
        }

        public static byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
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

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);

            return tokenString;
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
