using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Dtos
{
    public class ForgotPasswordRequestDto
    {
        [Required, MaxLength(50), EmailAddress]
        public string Email { get; set; } = null!;
    }
}
