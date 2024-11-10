using DataAPI.DTOs.Experience;
using DataAPI.Services.Implementations;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpGet("getAllExperiences")]
    public async Task<IActionResult> GetAllBlogPosts()
    {
        var experiences = await _experienceService.GetAllExperiencesAsync();
        return Ok(experiences);
    }

    [HttpGet("getExperience/{id}")]
    public async Task<IActionResult> GetExperienceById([FromRoute] int id)
    {
        var experience = await _experienceService.GetExperienceByIdAsync(id);

        if (experience == null)
            return NotFound("Experience not found");

        return Ok(experience);
    }

    [HttpPost("createExperiences")]
    public async Task<IActionResult> CreateExperiences([FromBody] CreateExperienceDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newExperience = await _experienceService.CreateExperienceAsync(createDto);
        return Ok(newExperience);
    }

    [HttpPut("updateExperience")]
    public async Task<IActionResult> UpdateExperiences([FromBody] UpdateExperienceDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedExperience = await _experienceService.UpdateExperienceAsync(updateDto);

        if (updatedExperience == null)
            return NotFound("Experience not found");

        return Ok(updatedExperience);
    }

    [HttpDelete("deleteExperience/{id}")]
    public async Task<IActionResult> DeleteExperience([FromRoute] int id)
    {
        var isDeleted = await _experienceService.DeleteExperienceAsync(id);

        if (!isDeleted)
            return NoContent();

        return Ok();
    }
}
