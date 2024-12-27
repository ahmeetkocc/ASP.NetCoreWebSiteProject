namespace Core.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}
