using DataAPI.DTOs.AboutMe;

namespace DataAPI.Services.Interfaces;

public interface IAboutMeService
{
    Task<DetailsAboutMeDto?> GetAboutMeAsync();
    Task<bool> CreateAboutMeAsync(CreateAboutMeDto createDto);
    Task<DetailsAboutMeDto?> UpdateAboutMeAsync(UpdateAboutMeDto updateDto);
}
