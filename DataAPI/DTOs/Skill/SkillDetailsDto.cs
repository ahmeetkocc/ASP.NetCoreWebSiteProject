using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skill;

public class SkillDetailsDto
{
    [Required]
    public List<string> SkillsList { get; set; }
}
