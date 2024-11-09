using DataAPI.DTOs.PersonalInfo;

namespace DataAPI.Services.Interfaces;

public interface IPersonalInfoService
{
    Task<CreatePersonalInfoDto?> GetPersonalInfoAsync();
    Task<CreatePersonalInfoDto> CreatePersonalInfoAsync(CreatePersonalInfoDto createDto);
    Task<bool> UpdatePersonalInfoAsync(UpdatePersonalInfoDto updateDto);
}
