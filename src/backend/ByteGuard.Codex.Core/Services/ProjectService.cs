using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Core.Services;

public class ProjectService
{
    private readonly ICodexDbContext _context;

    public ProjectService(ICodexDbContext context)
    {
        _context = context;
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
                    Requirement = r.AsvsRequirement != null ?
                        new RequirementDetails
                        {
                            Id = r.AsvsRequirement.Id,
                            Code = r.AsvsRequirement.Code,
                            Ordinal = r.AsvsRequirement.Ordinal,
                            Description = r.AsvsRequirement.Description,
                            Level = r.AsvsRequirement.Level
                        } :
                        new RequirementDetails
                        {
                            Id = r.CustomRequirement!.Id,
                            Code = r.CustomRequirement.Code,
                            Ordinal = r.CustomRequirement.Ordinal,
                            Description = r.CustomRequirement.Description,
                            Level = r.CustomRequirement.Level
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
        var id = Guid.NewGuid();
        var project = new Project(id);
        project.Title = title;
        project.Owner = owner;
        project.AsvsVersionId = asvsVersionId;

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

    public async Task AddRequirementAsync(Guid projectId, Guid requirementId)
    {
        throw new NotImplementedException();
    }

    public async Task AddRequirementsAsync(Guid projectId, List<Guid> requirementIds)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateProjectStatusAsync(Guid proejctId, ProjectStatus newStatus)
    {
        throw new NotImplementedException();
    }
}
