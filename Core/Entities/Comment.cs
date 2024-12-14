namespace Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsApproved { get; set; }
    public int BlogPostId { get; set; }

    public BlogPost BlogPost { get; set; }
    //public virtual User User { get; set; }
}
