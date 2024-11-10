using DataAPI.DTOs.PersonalInfo;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonalInfoController : ControllerBase
{
    private readonly IPersonalInfoService _personalDetailService;

    public PersonalInfoController(IPersonalInfoService personalDetailService)
    {
        _personalDetailService = personalDetailService;
    }

    [HttpGet("getPersonalInfo")]
    public async Task<IActionResult> GetPersonalInfo()
    {
        var personalDetail = await _personalDetailService.GetPersonalInfoAsync();

        if (personalDetail == null)
            return NotFound("Personal detail not found");

        return Ok(personalDetail);
    }

    [HttpPost("createPersonalInfo")]
    public async Task<IActionResult> CreatePersonalInfo([FromBody] CreatePersonalInfoDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newPersonalInfo = await _personalDetailService.CreatePersonalInfoAsync(createDto);
        return Ok(newPersonalInfo);
    }

    [HttpPut("updatePersonalInfo")]
    public async Task<IActionResult> UpdatePersonalInfo([FromBody] UpdatePersonalInfoDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isUpdated = await _personalDetailService.UpdatePersonalInfoAsync(updateDto);

        if (!isUpdated)
            return NotFound("Personal detail not found");

        return NoContent();
    }
}
