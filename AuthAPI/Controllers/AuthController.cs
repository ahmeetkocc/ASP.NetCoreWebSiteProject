using Data.AuthContext;
using Data.AuthEntities;
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
using AuthAPI.Dtos;
using AuthAPI.Services.Interfaces;

namespace AuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;

	public AuthController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpGet("user/{userId}")]
	public async Task<IActionResult> GetUser([FromRoute] int userId)
	{
		var user = await _authService.GetUserByIdAsync(userId);
		if (user == null)
			return BadRequest("Kullanıcı bulunamadı.");

		return Ok(user);
	}

	[HttpGet("userList")]
	public async Task<IActionResult> GetUsers()
	{
		var userList = await _authService.GetAllUsersAsync();
		return Ok(userList);
	}

	[HttpPut("updateUser")]
	public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateDto)
	{
		var tokenResponse = await _authService.UpdateUserAsync(updateDto);
		if (tokenResponse == null)
			return BadRequest("Kullanıcı bulunamadı.");

		return Ok(tokenResponse);
	}

	[HttpDelete("delete/{userId}")]
	public async Task<IActionResult> DeleteUser([FromRoute] int userId)
	{
		var result = await _authService.DeleteUserAsync(userId);
		if (!result)
			return NoContent();

		return Ok();
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginDto dto)
	{
		var tokenResponse = await _authService.LoginAsync(dto);
		if (tokenResponse == null)
			return BadRequest("Kullanıcı adı veya şifre yanlış.");

		return Ok(tokenResponse);
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
	{
		var result = await _authService.RegisterAsync(registerDto);
		if (!result)
			return BadRequest("Bu email ile bir kullanıcı zaten mevcut.");

		return Ok();
	}

	[HttpGet("refresh")]
	public async Task<IActionResult> Refresh([FromQuery] string token)
	{
		var tokenResponse = await _authService.RefreshTokenAsync(token);
		if (tokenResponse == null)
			return Unauthorized("Geçersiz refresh token");

		return Ok(tokenResponse);
	}

	[HttpPost("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDto dto)
	{
		var result = await _authService.ForgotPasswordAsync(dto);
		if (!result)
			return BadRequest("Kullanıcı bulunamadı");

		return Ok();
	}

	[HttpPost("renew-password")]
	public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordRequestDto dto)
	{
		var result = await _authService.RenewPasswordAsync(dto);
		if (!result)
			return BadRequest("Geçersiz doğrulama kodu.");

		return Ok("Şifre başarıyla sıfırlandı.");
	}
}
