using DataAPI.DTOs.Comment;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
	private readonly ICommentService _commentService;

	public CommentController(ICommentService commentService)
	{
		_commentService = commentService;
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetComments([FromRoute] int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var comment = await _commentService.GetCommentByIdAsync(id);

		if (comment == null)
			return NotFound("Comments not found");

		return Ok(comment);
	}

	[HttpPost("createComment")]
	public async Task<IActionResult> CreateComments([FromBody] CreateCommentDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var newComment = await _commentService.CreateCommentAsync(createDto);
		return Ok(newComment);
	}

	[HttpPut("updateComment")]
	public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updatedComment = await _commentService.UpdateCommentAsync(updateDto);

		if (updatedComment == null)
			return NotFound("Comment not found");

		return Ok(updatedComment);
	}

	[HttpDelete("deleteComment/{id}")]
	public async Task<IActionResult> DeleteComments([FromRoute] int id)
	{
		var isDeleted = await _commentService.DeleteCommentAsync(id);

		if (!isDeleted)
			return NoContent();

		return Ok("Comment deleted");
	}
}
