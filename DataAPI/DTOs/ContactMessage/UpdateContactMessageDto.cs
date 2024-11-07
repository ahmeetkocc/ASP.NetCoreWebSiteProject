using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.ContactMessage;

public class UpdateContactMessageDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public string Message { get; set; } = string.Empty;

    [Required]
    public DateTime SentDate { get; set; }

    public bool IsRead { get; set; }

    public string Reply { get; set; } = string.Empty;

    public DateTime? ReplyDate { get; set; }
}
