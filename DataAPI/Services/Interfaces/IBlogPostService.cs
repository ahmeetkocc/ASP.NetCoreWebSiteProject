using DataAPI.DTOs.BlogPost;

namespace DataAPI.Services.Interfaces;

public interface IBlogPostService
{
	Task<List<BlogPostDto>> GetAllBlogPostsAsync();
	Task<BlogPostDto?> GetBlogPostByIdAsync(int id);
    Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostDto createDto);
    Task<BlogPostDto?> UpdateBlogPostAsync(UpdateBlogPostDto updateDto);
    Task<bool> DeleteBlogPostAsync(int id);
}

