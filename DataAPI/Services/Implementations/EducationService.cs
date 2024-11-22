using Data.DataEntities;
using Data.DataContext;
using DataAPI.DTOs.Education;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class EducationService : IEducationService
{
    private readonly AppDbContext _dataDbContext;

    public EducationService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<EducationDto?> GetEducationByIdAsync(int id)
    {
        var education = await _dataDbContext.Educations.FindAsync(id);

        if (education == null)
            return null;

        return new EducationDto
        {
            Id = education.Id,
            Degree = education.Degree,
            School = education.School,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        };
    }

    public async Task<List<EducationDto>> GetAllEducationsAsync()
    {
        var educations = await _dataDbContext.Educations.ToListAsync();

        return educations.Select(education => new EducationDto
        {
            Id = education.Id,
            Degree = education.Degree,
            School = education.School,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        }).ToList();
    }

    public async Task<EducationDto> CreateEducationAsync(CreateEducationDto createDto)
    {
        if (createDto.StartDate > createDto.EndDate)
            throw new ArgumentException("Start date cannot be later than end date.");

        var newEducation = new Education
        {
            Degree = createDto.Degree,
            School = createDto.School,
            StartDate = createDto.StartDate,
            EndDate = createDto.EndDate
        };

        _dataDbContext.Educations.Add(newEducation);
        await _dataDbContext.SaveChangesAsync();

        return new EducationDto
        {
            Id = newEducation.Id,
            Degree = newEducation.Degree,
            School = newEducation.School,
            StartDate = newEducation.StartDate,
            EndDate = newEducation.EndDate
        };
    }

    public async Task<EducationDto?> UpdateEducationAsync(UpdateEducationDto updateDto)
    {
        var existEducation = await _dataDbContext.Educations.FindAsync(updateDto.Id);

        if (existEducation == null)
            return null;

        if (updateDto.StartDate > updateDto.EndDate)
            throw new ArgumentException("Start date cannot be later than end date.");

        existEducation.Degree = updateDto.Degree;
        existEducation.School = updateDto.School;
        existEducation.StartDate = updateDto.StartDate;
        existEducation.EndDate = updateDto.EndDate;

        _dataDbContext.Educations.Update(existEducation);
        await _dataDbContext.SaveChangesAsync();

        return new EducationDto
        {
            Id = existEducation.Id,
            Degree = existEducation.Degree,
            School = existEducation.School,
            StartDate = existEducation.StartDate,
            EndDate = existEducation.EndDate
        };
    }

    public async Task<bool> DeleteEducationAsync(int id)
    {
        var existEducation = await _dataDbContext.Educations.FindAsync(id);

        if (existEducation == null)
            return false;

        _dataDbContext.Educations.Remove(existEducation);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
