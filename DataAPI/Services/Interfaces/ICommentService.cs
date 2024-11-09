using DataAPI.DTOs.Comment;

namespace DataAPI.Services.Interfaces;

public interface ICommentService
{
    Task<CommentDto?> GetCommentByIdAsync(int id);
    Task<CommentDto> CreateCommentAsync(CreateCommentDto createDto);
    Task<CommentDto?> UpdateCommentAsync(UpdateCommentDto updateDto);
    Task<bool> DeleteCommentAsync(int id);
}
