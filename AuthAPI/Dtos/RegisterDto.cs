using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    public string Username { get; set; }

    [Required, MaxLength(50), DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required, MaxLength(50), DataType(DataType.Password)]
    public string Password { get; set; }

    public string Project { get; set; }

}