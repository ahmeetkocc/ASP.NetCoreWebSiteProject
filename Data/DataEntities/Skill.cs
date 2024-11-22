using System.ComponentModel.DataAnnotations;

namespace Data.DataEntities;

public class Skill
{
	[Required]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }
}