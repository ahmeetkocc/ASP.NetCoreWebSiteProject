using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Education;

public class CreateEducationDto
{
    [Required]
    public string Degree { get; set; } = string.Empty;

    [Required]
    public string School { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime? EndDate { get; set; }
}
