namespace AuthAPI.Dtos;

public class GetUserResponseDto
{
	public int Id { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string RoleName { get; set; }
	public int RoleId { get; set; }
	public List<string> RefreshTokens { get; set; }
}
