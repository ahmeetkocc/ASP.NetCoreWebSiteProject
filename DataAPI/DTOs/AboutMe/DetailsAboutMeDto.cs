using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.AboutMe;

public class DetailsAboutMeDto
{
    [Required]
    public string Introduction { get; set; } = string.Empty;

    [Required]
    public string ImageUrl1 { get; set; } = string.Empty;

    [Required]
    public string ImageUrl2 { get; set; } = string.Empty;
}
