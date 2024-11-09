using DataAPI.DTOs.Project;

namespace DataAPI.Services.Interfaces;

public interface IProjectService
{
    Task<ProjectDto?> GetProjectByIdAsync(int id);
    Task<List<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto createDto);
    Task<ProjectDto?> UpdateProjectAsync(UpdateProjectDto updateDto);
    Task<bool> DeleteProjectAsync(int id);
}
