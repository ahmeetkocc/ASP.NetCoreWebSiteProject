namespace Core.Entities;

public class BlogPost : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public int AuthorId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}
