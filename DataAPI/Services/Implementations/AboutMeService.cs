using Data.DataEntities;
using Data.DataContext;
using DataAPI.DTOs.AboutMe;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class AboutMeService : IAboutMeService
{
    private readonly AppDbContext _dataDbContext;

    public AboutMeService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<DetailsAboutMeDto?> GetAboutMeAsync()
    {
        var aboutMe = await _dataDbContext.AboutMe.FirstOrDefaultAsync();

        if (aboutMe == null)
            return null;

        return new DetailsAboutMeDto
        {
            Introduction = aboutMe.Introduction,
            ImageUrl1 = aboutMe.ImageUrl1,
            ImageUrl2 = aboutMe.ImageUrl2
        };
    }

    public async Task<bool> CreateAboutMeAsync(CreateAboutMeDto createDto)
    {
        if (createDto == null) return false;

        var newAboutMe = new AboutMe
        {
            Introduction = createDto.Introduction,
            ImageUrl1 = createDto.ImageUrl1,
            ImageUrl2 = createDto.ImageUrl2
        };
        _dataDbContext.AboutMe.Add(newAboutMe);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<DetailsAboutMeDto?> UpdateAboutMeAsync(UpdateAboutMeDto updateDto)
    {
        if (updateDto == null) return null;

        var existAboutMe = await _dataDbContext.AboutMe.FirstOrDefaultAsync();

        if (existAboutMe == null)
            return null;

        existAboutMe.Introduction = updateDto.Introduction;
        existAboutMe.ImageUrl1 = updateDto.ImageUrl1;
        existAboutMe.ImageUrl2 = updateDto.ImageUrl2;

        _dataDbContext.AboutMe.Update(existAboutMe);
        await _dataDbContext.SaveChangesAsync();

        return new DetailsAboutMeDto
        {
            Introduction = existAboutMe.Introduction,
            ImageUrl1 = existAboutMe.ImageUrl1,
            ImageUrl2 = existAboutMe.ImageUrl2
        };
    }
}
