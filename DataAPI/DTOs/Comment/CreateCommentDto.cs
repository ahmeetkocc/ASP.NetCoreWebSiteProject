namespace DataAPI.DTOs.Comment;

public class CreateCommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedTime { get; set; }
    public bool IsApproved { get; set; }
    public int BlogPostId { get; set; }
}
