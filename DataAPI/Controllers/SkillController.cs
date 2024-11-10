using DataAPI.DTOs.Skill;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetSkills()
    {
        var skills = await _skillService.GetSkillsAsync();

        if (skills == null)
            return NotFound("Skills not found");

        return Ok(skills);
    }

    [HttpPost("createSkills")]
    public async Task<IActionResult> CreateSkills([FromBody] CreateSkillDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _skillService.CreateSkillsAsync(createDto);
        return Ok();
    }

    [HttpPut("updateSkills")]
    public async Task<IActionResult> UpdateSkills([FromBody] UpdateSkillsDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedSkills = await _skillService.UpdateSkillsAsync(updateDto);

        if (updatedSkills == null)
            return NotFound("Skill is not found");

        return Ok(updatedSkills);
    }

    [HttpDelete("deleteSkill/{skillName}")]
    public async Task<IActionResult> DeleteSkill([FromRoute] string skillName)
    {
        var isDeleted = await _skillService.DeleteSkillAsync(skillName);

        if (!isDeleted)
            return NotFound("Skill not found");

        return Ok();
    }
}
