namespace AuthAPI.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public bool IsRevoked { get; set; }
    public int UserId { get; set; }

    public virtual User User { get; set; }
}
