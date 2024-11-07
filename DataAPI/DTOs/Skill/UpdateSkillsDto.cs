using System.ComponentModel.DataAnnotations;

namespace DataAPI.DTOs.Skill;

public class UpdateSkillsDto
{
    [Required]
    public List<string> SkillsList { get; set; }
}