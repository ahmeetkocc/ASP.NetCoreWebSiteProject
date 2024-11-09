using DataAPI.DTOs.Education;

namespace DataAPI.Services.Interfaces;

public interface IEducationService
{
    Task<EducationDto?> GetEducationByIdAsync(int id);
    Task<List<EducationDto>> GetAllEducationsAsync();
    Task<EducationDto> CreateEducationAsync(CreateEducationDto createDto);
    Task<EducationDto?> UpdateEducationAsync(UpdateEducationDto updateDto);
    Task<bool> DeleteEducationAsync(int id);
}
