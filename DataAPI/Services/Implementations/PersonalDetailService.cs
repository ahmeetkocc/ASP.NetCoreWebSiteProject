using Core.Entities;
using Data.DataContext;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataAPI.DTOs.PersonalInfo;

namespace DataAPI.Services.Implementations;

public class PersonalInfoService : IPersonalInfoService
{
    private readonly AppDbContext _dataDbContext;

    public PersonalInfoService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<CreatePersonalInfoDto?> GetPersonalInfoAsync()
    {
        var personalDetail = await _dataDbContext.PersonalInfos.FirstOrDefaultAsync();

        if (personalDetail == null)
            return null;

        return new CreatePersonalInfoDto
        {
            About = personalDetail.About,
            Name = personalDetail.Name,
            Surname = personalDetail.Surname,
            BirthDate = personalDetail.BirthDate
        };
    }

    public async Task<CreatePersonalInfoDto> CreatePersonalInfoAsync(CreatePersonalInfoDto createDto)
    {
        var newPersonalInfo = new PersonalInfo
        {
            About = createDto.About,
            Name = createDto.Name,
            Surname = createDto.Surname,
            BirthDate = createDto.BirthDate
        };

        _dataDbContext.PersonalInfos.Add(newPersonalInfo);
        await _dataDbContext.SaveChangesAsync();

        return createDto;
    }

    public async Task<bool> UpdatePersonalInfoAsync(UpdatePersonalInfoDto updateDto)
    {
        var existPersonalInfo = await _dataDbContext.PersonalInfos.FirstOrDefaultAsync();

        if (existPersonalInfo == null)
            return false;

        existPersonalInfo.About = updateDto.About;
        existPersonalInfo.Name = updateDto.Name;
        existPersonalInfo.Surname = updateDto.Surname;
        existPersonalInfo.BirthDate = updateDto.BirthDate;

        _dataDbContext.Update(existPersonalInfo);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
