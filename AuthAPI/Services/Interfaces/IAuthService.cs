using AuthAPI.Dtos;

namespace AuthAPI.Services.Interfaces;

public interface IAuthService
{
	Task<GetUserResponseDto?> GetUserByIdAsync(int userId);
	Task<List<GetUserResponseDto>> GetAllUsersAsync();
	Task<TokenResponseDto?> UpdateUserAsync(UpdateUserDto updateDto);
	Task<bool> DeleteUserAsync(int userId);
	Task<TokenResponseDto?> LoginAsync(LoginDto dto);
	Task<bool> RegisterAsync(RegisterDto registerDto);
	Task<TokenResponseDto?> RefreshTokenAsync(string token);
	Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto dto);
	Task<bool> RenewPasswordAsync(RenewPasswordRequestDto dto);
}
