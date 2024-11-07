using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skill;

public class CreateSkillDto
{
    [Required]
    public List<string> SkillsList { get; set; }
}
