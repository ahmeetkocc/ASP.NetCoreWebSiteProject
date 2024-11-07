namespace DataAPI.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsApproved { get; set; }
    public int BlogPostId { get; set; }
}
