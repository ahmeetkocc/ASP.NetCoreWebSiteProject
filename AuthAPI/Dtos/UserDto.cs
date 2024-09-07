public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public string? ResetPasswordToken { get; set; }
    public int RoleId { get; set; }
}
