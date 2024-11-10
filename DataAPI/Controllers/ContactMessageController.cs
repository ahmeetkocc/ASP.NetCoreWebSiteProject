using DataAPI.DTOs.ContactMessage;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactMessageController : ControllerBase
{
    private readonly IContactMessageService _contactMessageService;

    public ContactMessageController(IContactMessageService contactMessageService)
    {
        _contactMessageService = contactMessageService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactMessages([FromRoute] int id)
    {
        var contactMessage = await _contactMessageService.GetContactMessageByIdAsync(id);

        if (contactMessage == null)
            return NotFound("Contact message not found");

        return Ok(contactMessage);
    }

    [HttpPost("createContactMessages")]
    public async Task<IActionResult> CreateContactMessages([FromBody] CreateContactMessageDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newContactMessage = await _contactMessageService.CreateContactMessageAsync(createDto);
        return Ok(newContactMessage);
    }

    [HttpPut("updateContactMessages/{id}")]
    public async Task<IActionResult> UpdateContactMessages([FromBody] UpdateContactMessageDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedContactMessage = await _contactMessageService.UpdateContactMessageAsync(updateDto);

        if (updatedContactMessage == null)
            return NotFound("Contact message not found");

        return Ok(updatedContactMessage);
    }

    [HttpDelete("deleteContactMessages/{id}")]
    public async Task<IActionResult> DeleteContactMessages([FromRoute] int id)
    {
        var isDeleted = await _contactMessageService.DeleteContactMessageAsync(id);

        if (!isDeleted)
            return NotFound("Contact message not found");

        return Ok();
    }
}
