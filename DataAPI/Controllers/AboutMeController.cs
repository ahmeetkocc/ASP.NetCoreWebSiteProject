using DataAPI.DTOs.AboutMe;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutMeController : ControllerBase
{
	private readonly IAboutMeService _aboutMeService;

	public AboutMeController(IAboutMeService aboutMeService)
	{
		_aboutMeService = aboutMeService;
	}

	[HttpGet("getAboutMe")]
	public async Task<IActionResult> GetAboutMe()
	{
		var aboutMeDto = await _aboutMeService.GetAboutMeAsync();

		if (aboutMeDto == null)
			return NotFound("User not found");

		return Ok(aboutMeDto);
	}

	[HttpPost("createAboutMe")]
	public async Task<IActionResult> PostAboutMe([FromBody] CreateAboutMeDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var result = await _aboutMeService.CreateAboutMeAsync(createDto);

		if (!result)
			return BadRequest("Failed to create AboutMe");

		return Ok();
	}

	[HttpPut("updateAboutMe")]
	public async Task<IActionResult> UpdateAboutMe([FromBody] UpdateAboutMeDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updatedAboutMe = await _aboutMeService.UpdateAboutMeAsync(updateDto);

		if (updatedAboutMe == null)
			return NotFound("AboutMe is not found");

		return Ok(updatedAboutMe);
	}
}
