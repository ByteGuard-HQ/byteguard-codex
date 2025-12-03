using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Core.Services;

/// <summary>
/// Core logic when handling project specific classes.
/// </summary>
public class ProjectService
{
    private readonly ICodexDbContext _context;

    /// <summary>
    /// Create a new <see cref="ProjectService"/> instance.
    /// </summary>
    /// <param name="context">Codex database context.</param>
    public ProjectService(ICodexDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all projects metadata.
    /// </summary>
    /// <returns>List of <see cref="ProjectMetadata"/>.</returns>
    public async Task<List<ProjectMetadata>> GetProjectsAsync()
    {
        var projects = await _context.Projects
            .AsNoTracking()
            .Select(p => new ProjectMetadata
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                Owner = p.Owner,
                Status = p.Status,
                CreatedAt = p.CreatedAt,
                ArchivedAt = p.ArchivedAt,
                AsvsVersionId = p.AsvsVersionId
            }).ToListAsync();

        return projects;
    }

    /// <summary>
    /// Get project details.
    /// </summary>
    /// <param name="projectId">Project identifier.</param>
    /// <returns><see cref="ProjectDetails"/> if found, <c>null</c> otherwise.</returns>
    public async Task<ProjectDetails?> GetProjectAsync(Guid projectId)
    {
        var project = await _context.Projects
            .Include(p => p.Requirements)
            .AsNoTracking()
            .Select(x => new ProjectDetails
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Owner = x.Owner,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
                ArchivedAt = x.ArchivedAt,
                AsvsVersionId = x.AsvsVersionId,
                Requirements = x.Requirements.Select(r => new ProjectRequirementDetails
                {
                    Id = r.Id,
                    Status = r.Status,
                    Requirement = new AsvsRequirementDetails
                    {
                        Id = r.AsvsRequirement.Id,
                        Code = r.AsvsRequirement.Code.ToVersionString(),
                        Ordinal = r.AsvsRequirement.Ordinal,
                        Description = r.AsvsRequirement.Description,
                        Level = r.AsvsRequirement.Level
                    }
                }).ToList().AsReadOnly()
            })
            .FirstOrDefaultAsync(x => x.Id.Equals(projectId));

        return project;
    }

    /// <summary>
    /// Create new project.
    /// </summary>
    /// <param name="title">Title.</param>
    /// <param name="owner">Project owner.</param>
    /// <param name="asvsVersionId">The AVSS version this project is comitting to.</param>
    /// <returns>The newly created <see cref="ProjectDetails"/>.</returns>
    public async Task<ProjectDetails> CreateProjectAsync(string title, string owner, Guid asvsVersionId)
    {
        var project = new Project()
        {
            Title = title,
            Status = ProjectStatus.Active,
            CreatedAt = DateTime.UtcNow,
            Owner = owner,
            AsvsVersionId = asvsVersionId
        };

        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        var result = new ProjectDetails
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Owner = project.Owner,
            Status = project.Status,
            CreatedAt = project.CreatedAt,
            AsvsVersionId = project.AsvsVersionId
        };

        return result;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="requirementId"></param>
    /// <returns></returns>
    public async Task AddRequirementAsync(Guid projectId, Guid requirementId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="requirementIds"></param>
    /// <returns></returns>
    public async Task AddRequirementsAsync(Guid projectId, List<Guid> requirementIds)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="newStatus"></param>
    /// <returns></returns>
    public async Task UpdateProjectStatusAsync(Guid projectId, ProjectStatus newStatus)
    {
        throw new NotImplementedException();
    }
}
