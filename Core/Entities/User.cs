namespace Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public string? ResetPasswordToken { get; set; }
    public int RoleId { get; set; }

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    public virtual Role Role { get; set; }
}
