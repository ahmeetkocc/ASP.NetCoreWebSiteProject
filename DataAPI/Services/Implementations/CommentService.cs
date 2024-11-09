using DataAPI.Data;
using DataAPI.DTOs.Comment;
using DataAPI.Data.Entities;
using DataAPI.Services.Interfaces;

namespace DataAPI.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly AppDbContext _dataDbContext;

    public CommentService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<CommentDto?> GetCommentByIdAsync(int id)
    {
        var comment = await _dataDbContext.Comments.FindAsync(id);

        if (comment == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            CreatedDate = comment.CreatedDate,
            IsApproved = comment.IsApproved,
            BlogPostId = comment.BlogPostId
        };
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createDto)
    {
        var newComment = new Comment
        {
            Content = createDto.Content,
            CreatedDate = createDto.CreatedTime,
            IsApproved = createDto.IsApproved,
            BlogPostId = createDto.BlogPostId
        };

        _dataDbContext.Comments.Add(newComment);
        await _dataDbContext.SaveChangesAsync();

        return new CommentDto
        {
            Id = newComment.Id,
            Content = newComment.Content,
            CreatedDate = newComment.CreatedDate,
            IsApproved = newComment.IsApproved,
            BlogPostId = newComment.BlogPostId
        };
    }

    public async Task<CommentDto?> UpdateCommentAsync(UpdateCommentDto updateDto)
    {
        var existComment = await _dataDbContext.Comments.FindAsync(updateDto.Id);

        if (existComment == null)
            return null;

        existComment.Content = updateDto.Content;
        existComment.CreatedDate = updateDto.CreatedTime;
        existComment.IsApproved = updateDto.IsApproved;
        existComment.BlogPostId = updateDto.BlogPostId;

        _dataDbContext.Comments.Update(existComment);
        await _dataDbContext.SaveChangesAsync();

        return new CommentDto
        {
            Id = existComment.Id,
            Content = existComment.Content,
            CreatedDate = existComment.CreatedDate,
            IsApproved = existComment.IsApproved,
            BlogPostId = existComment.BlogPostId
        };
    }

    public async Task<bool> DeleteCommentAsync(int id)
    {
        var existComment = await _dataDbContext.Comments.FindAsync(id);

        if (existComment == null)
            return false;

        _dataDbContext.Comments.Remove(existComment);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
