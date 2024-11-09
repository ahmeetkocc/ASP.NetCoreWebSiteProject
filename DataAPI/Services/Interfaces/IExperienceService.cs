using DataAPI.DTOs.Experience;

namespace DataAPI.Services.Interfaces;

public interface IExperienceService
{
    Task<List<DetailsExperienceDto>> GetAllExperiencesAsync();
    Task<DetailsExperienceDto?> GetExperienceByIdAsync(int id);
    Task<DetailsExperienceDto> CreateExperienceAsync(CreateExperienceDto createDto);
    Task<DetailsExperienceDto?> UpdateExperienceAsync(UpdateExperienceDto updateDto);
    Task<bool> DeleteExperienceAsync(int id);
}
