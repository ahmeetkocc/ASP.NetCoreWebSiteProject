using Core.Entities;
using Data.DataContext;
using DataAPI.DTOs.BlogPost;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class BlogPostService : IBlogPostService
{
	private readonly AppDbContext _dataDbContext;

	public BlogPostService(AppDbContext dataDbContext)
	{
		_dataDbContext = dataDbContext;
	}

	public async Task<List<BlogPostDto>> GetAllBlogPostsAsync()
	{
		var blogPosts = await _dataDbContext.BlogPosts.ToListAsync();

		return blogPosts.Select(blogPost => new BlogPostDto
		{
			Id = blogPost.Id,
			Title = blogPost.Title,
			Content = blogPost.Content,
			PublishDate = blogPost.PublishDate,
			AuthorId = blogPost.AuthorId
		}).ToList();
	}

	public async Task<BlogPostDto?> GetBlogPostByIdAsync(int id)
	{
		var blogPost = await _dataDbContext.BlogPosts.FindAsync(id);

		if (blogPost == null)
			return null;

		return new BlogPostDto
		{
			Id = blogPost.Id,
			Title = blogPost.Title,
			Content = blogPost.Content,
			PublishDate = blogPost.PublishDate,
			AuthorId = blogPost.AuthorId
		};
	}

	public async Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostDto createDto)
	{
		var newBlogPost = new BlogPost
		{
			Title = createDto.Title,
			Content = createDto.Content,
			PublishDate = createDto.PublishDate,
			AuthorId = createDto.AuthorId
		};

		_dataDbContext.BlogPosts.Add(newBlogPost);
		await _dataDbContext.SaveChangesAsync();

		return new BlogPostDto
		{
			Id = newBlogPost.Id,
			Title = newBlogPost.Title,
			Content = newBlogPost.Content,
			PublishDate = newBlogPost.PublishDate,
			AuthorId = newBlogPost.AuthorId
		};
	}

	public async Task<BlogPostDto?> UpdateBlogPostAsync(UpdateBlogPostDto updateDto)
	{
		var existBlogPost = await _dataDbContext.BlogPosts.FindAsync(updateDto.Id);

		if (existBlogPost == null)
			return null;

		existBlogPost.Title = updateDto.Title;
		existBlogPost.Content = updateDto.Content;
		existBlogPost.PublishDate = updateDto.PublishDate;
		existBlogPost.AuthorId = updateDto.AuthorId;

		_dataDbContext.BlogPosts.Update(existBlogPost);
		await _dataDbContext.SaveChangesAsync();

		return new BlogPostDto
		{
			Id = existBlogPost.Id,
			Title = existBlogPost.Title,
			Content = existBlogPost.Content,
			PublishDate = existBlogPost.PublishDate,
			AuthorId = existBlogPost.AuthorId
		};
	}

	public async Task<bool> DeleteBlogPostAsync(int id)
	{
		var existBlogPost = await _dataDbContext.BlogPosts.FindAsync(id);

		if (existBlogPost == null)
			return false;

		_dataDbContext.BlogPosts.Remove(existBlogPost);
		await _dataDbContext.SaveChangesAsync();

		return true;
	}
}
