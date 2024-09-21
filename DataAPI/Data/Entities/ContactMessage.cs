namespace DataAPI.Data.Entities;

public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public DateTime SentDate { get; set; }
    public bool IsRead { get; set; }
    public string Reply { get; set; }
    public DateTime? ReplyDate { get; set; }
}
