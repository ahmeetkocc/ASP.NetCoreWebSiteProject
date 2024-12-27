using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Skill : BaseEntity
{
    [Required]
    public string Name { get; set; }
}