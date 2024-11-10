using DataAPI.DTOs.Project;
using DataAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject([FromRoute] int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);

        if (project == null)
            return NotFound("Project not found");

        return Ok(project);
    }

    [HttpGet("getAllProjects")]
    public async Task<IActionResult> GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpPost("createProject")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var newProject = await _projectService.CreateProjectAsync(createDto);
        return Ok(newProject);
    }

    [HttpPut("updateProject")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedProject = await _projectService.UpdateProjectAsync(updateDto);

        if (updatedProject == null)
            return NotFound("Project not found");

        return Ok(updatedProject);
    }

    [HttpDelete("deleteProject/{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        var isDeleted = await _projectService.DeleteProjectAsync(id);

        if (!isDeleted)
            return NotFound("Project not found");

        return Ok();
    }
}
