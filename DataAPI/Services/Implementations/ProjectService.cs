using Core.Entities;
using Data.DataContext;
using DataAPI.DTOs.Project;
using DataAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly AppDbContext _dataDbContext;

    public ProjectService(AppDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int id)
    {
        var project = await _dataDbContext.Projects.FindAsync(id);

        if (project == null)
            return null;

        return new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            ImageUrl = project.ImageUrl
        };
    }

    public async Task<List<ProjectDto>> GetAllProjectsAsync()
    {
        var projects = await _dataDbContext.Projects.ToListAsync();

        return projects.Select(project => new ProjectDto
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            ImageUrl = project.ImageUrl
        }).ToList();
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto createDto)
    {
        var newProject = new Project
        {
            Title = createDto.Title,
            Description = createDto.Description,
            ImageUrl = createDto.ImageUrl
        };

        _dataDbContext.Projects.Add(newProject);
        await _dataDbContext.SaveChangesAsync();

        return new ProjectDto
        {
            Id = newProject.Id,
            Title = newProject.Title,
            Description = newProject.Description,
            ImageUrl = newProject.ImageUrl
        };
    }

    public async Task<ProjectDto?> UpdateProjectAsync(UpdateProjectDto updateDto)
    {
        var existProject = await _dataDbContext.Projects.FindAsync(updateDto.Id);

        if (existProject == null)
            return null;

        existProject.Title = updateDto.Title;
        existProject.Description = updateDto.Description;
        existProject.ImageUrl = updateDto.ImageUrl;

        _dataDbContext.Projects.Update(existProject);
        await _dataDbContext.SaveChangesAsync();

        return new ProjectDto
        {
            Id = existProject.Id,
            Title = existProject.Title,
            Description = existProject.Description,
            ImageUrl = existProject.ImageUrl
        };
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var existProject = await _dataDbContext.Projects.FindAsync(id);

        if (existProject == null)
            return false;

        _dataDbContext.Projects.Remove(existProject);
        await _dataDbContext.SaveChangesAsync();

        return true;
    }
}
