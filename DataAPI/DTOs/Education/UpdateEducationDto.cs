using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Education;

public class UpdateEducationDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Degree { get; set; } = string.Empty;

    [Required]
    public string School { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime? EndDate { get; set; }
}
