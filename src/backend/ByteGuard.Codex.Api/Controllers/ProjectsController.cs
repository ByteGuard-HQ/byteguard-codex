using ByteGuard.Codex.Api.Parameters;
using ByteGuard.Codex.Core.Models;
using ByteGuard.Codex.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ByteGuard.Codex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get projects
        /// </summary>
        /// <response code="200">Returns metadata about all projects.</response>
        [HttpGet]
        [ProducesResponseType<List<ProjectMetadata>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();

            return Ok(projects);
        }

        /// <summary>
        /// Get project
        /// </summary>
        /// <param name="id">Project identifier.</param>
        /// <response code="200">Returns the project details.</response>
        /// <response code="400">The request is invalid.</response>
        /// <response code="404">Project with the given identifier could not be found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType<ProjectDetails>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var project = await _projectService.GetProjectAsync(id);
            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        /// <summary>
        /// Create project
        /// </summary>
        /// <param name="parameters">Project details.</param>
        /// <response code="201">Project was successfully created. The project details can be found in the body of the response.</response>
        /// <response code="400">The request is invalid.</response>
        [HttpPost]
        [ProducesResponseType<ProjectDetails>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProject(CreateProjectParameters parameters)
        {
            var project = await _projectService.CreateProjectAsync(
                parameters.Title,
                parameters.Owner,
                parameters.AsvsVersionId);

            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
    }
}
