using DataAPI.DTOs.Education;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class EducationController : ControllerBase
{
	private readonly IEducationService _educationService;

	public EducationController(IEducationService educationService)
	{
		_educationService = educationService;
	}

	[HttpGet("getEducation/{id}")]
	public async Task<IActionResult> GetEducationById([FromRoute] int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var education = await _educationService.GetEducationByIdAsync(id);

		if (education == null)
			return NotFound("Education not found");

		return Ok(education);
	}

	[HttpGet("getAllEducations")]
	public async Task<IActionResult> GetAllEducations()
	{
		var educations = await _educationService.GetAllEducationsAsync();
		return Ok(educations);
	}

	[HttpPost("createEducation")]
	public async Task<IActionResult> CreateEducation([FromBody] CreateEducationDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var newEducation = await _educationService.CreateEducationAsync(createDto);
		return Ok(newEducation);
	}

	[HttpPut("updateEducation")]
	public async Task<IActionResult> UpdateEducation([FromBody] UpdateEducationDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updatedEducation = await _educationService.UpdateEducationAsync(updateDto);

		if (updatedEducation == null)
			return NotFound("Education not found");

		return Ok(updatedEducation);
	}

	[HttpDelete("deleteEducation/{id}")]
	public async Task<IActionResult> DeleteEducation([FromRoute] int id)
	{
		var isDeleted = await _educationService.DeleteEducationAsync(id);

		if (!isDeleted)
			return NoContent();

		return Ok();
	}
}

