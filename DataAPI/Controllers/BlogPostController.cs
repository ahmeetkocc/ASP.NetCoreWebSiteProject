using DataAPI.DTOs.BlogPost;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class BlogPostController : ControllerBase
{
	private readonly IBlogPostService _blogPostService;

	public BlogPostController(IBlogPostService blogPostService)
	{
		_blogPostService = blogPostService;
	}

	[HttpGet("getBlogPost/{id}")]
	public async Task<IActionResult> GetBlogPostById([FromRoute] int id)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var blogPost = await _blogPostService.GetBlogPostByIdAsync(id);

		if (blogPost == null)
			return NotFound("BlogPost not found");

		return Ok(blogPost);
	}

	[HttpGet("getAllBlogPosts")]
	public async Task<IActionResult> GetAllBlogPosts()
	{
		var blogPosts = await _blogPostService.GetAllBlogPostsAsync();
		return Ok(blogPosts);
	}

	[HttpPost("createBlogPost")]
	public async Task<IActionResult> CreateBlogPosts([FromBody] CreateBlogPostDto createDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var newBlogPost = await _blogPostService.CreateBlogPostAsync(createDto);
		return Ok(newBlogPost);
	}

	[HttpPut("updateBlogPost")]
	public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostDto updateDto)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);

		var updatedBlogPost = await _blogPostService.UpdateBlogPostAsync(updateDto);

		if (updatedBlogPost == null)
			return NotFound("BlogPost not found");

		return Ok(updatedBlogPost);
	}

	[HttpDelete("deleteBlogPost/{id}")]
	public async Task<IActionResult> DeleteBlogPost([FromRoute] int id)
	{
		var isDeleted = await _blogPostService.DeleteBlogPostAsync(id);

		if (!isDeleted)
			return NoContent();

		return Ok();
	}
}
