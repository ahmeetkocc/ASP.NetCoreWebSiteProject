using Data.DataEntities;
using Data.DataContext;
using DataAPI.DTOs.Experience;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class ExperienceService : IExperienceService
{
    private readonly AppDbContext _dataDbContext;

    public ExperienceService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<List<DetailsExperienceDto>> GetAllExperiencesAsync()
    {
        var experiences = await _dataDbContext.Experiences.ToListAsync();

        return experiences.Select(experience => new DetailsExperienceDto
        {
            Id = experience.Id,
            Title = experience.Title,
            Company = experience.Company,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description
        }).ToList();
    }

    public async Task<DetailsExperienceDto?> GetExperienceByIdAsync(int id)
    {
        var experience = await _dataDbContext.Experiences.FindAsync(id);

        if (experience == null)
            return null;

        return new DetailsExperienceDto
        {
            Id = experience.Id,
            Title = experience.Title,
            Company = experience.Company,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description
        };
    }

    public async Task<DetailsExperienceDto> CreateExperienceAsync(CreateExperienceDto createDto)
    {
        var newExperience = new Experience
        {
            Title = createDto.Title,
            Company = createDto.Company,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate,
            Description = createDto.Description
        };

        _dataDbContext.Experiences.Add(newExperience);
        await _dataDbContext.SaveChangesAsync();

        return new DetailsExperienceDto
        {
            Title = newExperience.Title,
            Company = newExperience.Company,
            StartDate = newExperience.StartDate,
            EndDate = newExperience.EndDate,
            Description = newExperience.Description
        };
    }

    public async Task<DetailsExperienceDto?> UpdateExperienceAsync(UpdateExperienceDto updateDto)
    {
        var existExperience = await _dataDbContext.Experiences.FindAsync(updateDto.Id);

        if (existExperience == null)
            return null;

        existExperience.Title = updateDto.Title;
        existExperience.Company = updateDto.Company;
        existExperience.StartDate = updateDto.StartDate;
        existExperience.EndDate = updateDto.EndDate;
        existExperience.Description = updateDto.Description;

        _dataDbContext.Experiences.Update(existExperience);
        await _dataDbContext.SaveChangesAsync();

        return new DetailsExperienceDto
        {
            Id = existExperience.Id,
            Title = existExperience.Title,
            Company = existExperience.Company,
            StartDate = existExperience.StartDate,
            EndDate = existExperience.EndDate,
            Description = existExperience.Description
        };
    }

    public async Task<bool> DeleteExperienceAsync(int id)
    {
        var existExperience = await _dataDbContext.Experiences.FindAsync(id);

        if (existExperience == null)
            return false;

        _dataDbContext.Experiences.Remove(existExperience);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
