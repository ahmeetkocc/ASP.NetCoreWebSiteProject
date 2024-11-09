using DataAPI.DTOs.Skill;

namespace DataAPI.Services.Interfaces;

public interface ISkillService
{
    Task<SkillDetailsDto?> GetSkillsAsync();
    Task CreateSkillsAsync(CreateSkillDto createDto);
    Task<List<SkillDetailsDto>?> UpdateSkillsAsync(UpdateSkillsDto updateDto);
    Task<bool> DeleteSkillAsync(string skillName);
}
