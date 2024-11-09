using DataAPI.Data;
using DataAPI.DTOs.Skill;
using DataAPI.Data.Entities;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class SkillService : ISkillService
{
    private readonly AppDbContext _dataDbContext;

    public SkillService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<SkillDetailsDto?> GetSkillsAsync()
    {
        var skillsList = await _dataDbContext.Skills.ToListAsync();

        if (skillsList == null || !skillsList.Any())
            return null;

        return new SkillDetailsDto
        {
            SkillsList = skillsList.Select(s => s.Name).ToList()
        };
    }

    public async Task CreateSkillsAsync(CreateSkillDto createDto)
    {
        foreach (var skillName in createDto.SkillsList)
        {
            var newSkill = new Skill
            {
                Name = skillName
            };
            _dataDbContext.Skills.Add(newSkill);
        }

        await _dataDbContext.SaveChangesAsync();
    }

    public async Task<List<SkillDetailsDto>?> UpdateSkillsAsync(UpdateSkillsDto updateDto)
    {
        var existSkills = await _dataDbContext.Skills.ToListAsync();

        if (existSkills == null || !existSkills.Any())
            return null;

        var index = 0;
        foreach (var skillName in updateDto.SkillsList)
        {
            if (index == existSkills.Count)
                break;

            existSkills[index].Name = skillName;
            index++;
        }

        await _dataDbContext.SaveChangesAsync();

        return existSkills.Select(s => new SkillDetailsDto { SkillsList = existSkills.Select(s => s.Name).ToList() }).ToList();
    }

    public async Task<bool> DeleteSkillAsync(string skillName)
    {
        var existSkill = await _dataDbContext.Skills.FirstOrDefaultAsync(s => s.Name == skillName);

        if (existSkill == null)
            return false;

        _dataDbContext.Skills.Remove(existSkill);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
