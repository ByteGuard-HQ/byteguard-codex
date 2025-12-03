using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Enums;
using ByteGuard.Codex.Core.Exceptions;
using ByteGuard.Codex.Core.Models;
using ByteGuard.Codex.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Core.Services;

/// <summary>
/// Core logic for handling ASVS specific classes.
/// </summary>
public class AsvsService
{
    private readonly ICodexDbContext _context;

    /// <summary>
    /// Create a new <see cref="AsvsService"/> instance.
    /// </summary>
    /// <param name="context">Codex database context.</param>
    public AsvsService(ICodexDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all ASVS versions metadata currently registered.
    /// </summary>
    /// <returns>List of all available <see cref="AsvsVersionMetadata"/>.</returns>
    public async Task<List<AsvsVersionMetadata>> GetVersionsMetadataAsync()
    {
        var versionsMetadata = await _context.AsvsVersions
            .AsNoTracking()
            .Select(v => new AsvsVersionMetadata
            {
                Id = v.Id,
                VersionNumber = v.VersionNumber,
                Name = v.Name,
                Description = v.Description,
                IsReadOnly = v.IsReadOnly
            })
            .ToListAsync();

        return versionsMetadata;
    }

    /// <summary>
    /// Get ASVS version details.
    /// </summary>
    /// <param name="id">ASVS version identifier</param>
    /// <returns><see cref="AsvsVersionDetails"/> if found, <c>null</c> otherwise.</returns>
    public async Task<AsvsVersionDetails?> GetVersionAsync(Guid id)
    {
        var version = await _context.AsvsVersions
            .Include(v => v.AsvsChapters)
                .ThenInclude(c => c.AsvsSections)
            .AsNoTracking()
            .Select(v => new AsvsVersionDetails
            {
                Id = v.Id,
                VersionNumber = v.VersionNumber,
                Name = v.Name,
                Description = v.Description,
                IsReadOnly = v.IsReadOnly,
                Chapters = v.AsvsChapters.Select(c => new AsvsChapterDetails
                {
                    Id = c.Id,
                    Code = c.Code.ToVersionString(),
                    Ordinal = c.Ordinal,
                    Title = c.Title,
                    Description = c.Description,
                    Sections = c.AsvsSections.Select(s => new AsvsSectionDetails
                    {
                        Id = s.Id,
                        Code = s.Code.ToVersionString(),
                        Ordinal = s.Ordinal,
                        Name = s.Name,
                        Requirements = s.AsvsRequirements.Select(r => new AsvsRequirementDetails
                        {
                            Id = r.Id,
                            Code = r.Code.ToVersionString(),
                            Ordinal = r.Ordinal,
                            Description = r.Description,
                            Level = r.Level
                        }).OrderBy(r => r.Ordinal).ToList().AsReadOnly()
                    }).OrderBy(s => s.Ordinal).ToList().AsReadOnly()
                }).OrderBy(c => c.Ordinal).ToList().AsReadOnly()
            }).FirstOrDefaultAsync(x => x.Id.Equals(id));

        return version;
    }

    /// <summary>
    /// Create a new ASVS version.
    /// </summary>
    /// <param name="versionNumber">Version number.</param>
    /// <param name="name">Version name.</param>
    /// <param name="description">Optional version description.</param>
    /// <returns>The newly created <see cref="AsvsVersionMetadata"/>.</returns>
    public async Task<AsvsVersionMetadata> CreateAsvsVersionAsync(string versionNumber, string name, string? description)
    {
        var exists = await _context.AsvsVersions.AnyAsync(x => x.VersionNumber.Equals(versionNumber) && x.Name.Equals(name));
        if (exists)
        {
            throw new InvalidOperationException($"ASVS version with name '{name}' and version number '{versionNumber}' already exists.");
        }

        var version = new AsvsVersion()
        {
            VersionNumber = versionNumber,
            Name = name,
            Description = description
        };

        await _context.AsvsVersions.AddAsync(version);
        await _context.SaveChangesAsync();

        return new AsvsVersionMetadata
        {
            Id = version.Id,
            VersionNumber = version.VersionNumber,
            Name = version.Name,
            Description = version.Description,
            IsReadOnly = version.IsReadOnly
        };
    }

    /// <summary>
    /// Update ASVS version.
    /// </summary>
    /// <param name="id">ASVS version identifier.</param>
    /// <param name="versionNumber">New version number.</param>
    /// <param name="name">New name.</param>
    /// <param name="description">New description.</param>
    public async Task UpdateAsvsVersionAsync(Guid id, string versionNumber, string name, string? description)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (version is null)
        {
            throw new NotFoundException($"The ASVS version '{id}' could not be found.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and chapters cannot be added.");
        }

        var exists = await _context.AsvsVersions.AnyAsync(x => !x.Id.Equals(id) && x.VersionNumber.Equals(versionNumber) && x.Name.Equals(name));
        if (exists)
        {
            throw new InvalidOperationException($"ASVS version with name '{name}' and version number '{versionNumber}' already exists.");
        }

        version.VersionNumber = versionNumber;
        version.Name = name;
        version.Description = description;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create ASVS chapter.
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="title">Chapter title.</param>
    /// <param name="description">Optional chater description.</param>
    /// <returns>The newly created <see cref="AsvsChapterDetails"/>.</returns>
    public async Task<AsvsChapterDetails> CreateChapterAsync(Guid versionId, string title, string? description)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and chapters cannot be added.");
        }

        var currentOrdinal = await _context.AsvsChapters
            .Where(c => c.AsvsVersionId.Equals(versionId))
            .MaxAsync(c => (int?)c.Ordinal);

        var calculatedOrdinal = currentOrdinal is null ? 1 : currentOrdinal.Value + 1;

        var chapter = new AsvsChapter
        {
            Code = new AsvsCode(calculatedOrdinal),
            Ordinal = calculatedOrdinal,
            Title = title,
            Description = description,
            AsvsVersionId = versionId
        };

        await _context.AsvsChapters.AddAsync(chapter);
        await _context.SaveChangesAsync();

        return new AsvsChapterDetails
        {
            Id = chapter.Id,
            Code = chapter.Code.ToVersionString(),
            Ordinal = chapter.Ordinal,
            Title = chapter.Title,
            Description = chapter.Description,
            Sections = new List<AsvsSectionDetails>().AsReadOnly()
        };
    }

    /// <summary>
    /// Update ASVS chapter.
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="chapterId">Chapter identifier.</param>
    /// <param name="title">Chapter title.</param>
    /// <param name="description">Optional chapter description.</param>
    public async Task UpdateChapterAsync(Guid versionId, Guid chapterId, string title, string? description)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and chapters cannot be added.");
        }

        var chapter = await _context.AsvsChapters.FirstOrDefaultAsync(c => c.Id.Equals(chapterId) && c.AsvsVersionId.Equals(versionId));
        if (chapter is null)
        {
            throw new NotFoundException($"Chapter '{chapterId} does not exist on the ASVS version.");
        }

        chapter.Title = title;
        chapter.Description = description;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create ASVS section.
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="chapterId">Chapter identifier.</param>
    /// <param name="name">Section name.</param>
    /// <returns>The newly created <see cref="AsvsSectionDetails"/>.</returns>
    public async Task<AsvsSectionDetails> CreateSectionAsync(Guid versionId, Guid chapterId, string name)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and sections cannot be added.");
        }

        var chapter = await _context.AsvsChapters.FirstOrDefaultAsync(c => c.Id.Equals(chapterId) && c.AsvsVersionId.Equals(versionId));
        if (chapter is null)
        {
            throw new NotFoundException($"Chapter '{chapterId}' does not exist on the given ASVS version.");
        }

        var currentOrdinal = await _context.AsvsSections
            .Where(c => c.AsvsChapterId.Equals(chapterId) && c.AsvsChapter.AsvsVersionId.Equals(versionId))
            .MaxAsync(c => (int?)c.Ordinal);

        var calculatedOrdinal = currentOrdinal is null ? 1 : currentOrdinal.Value + 1;

        var section = new AsvsSection
        {
            Code = new AsvsCode(chapter.Ordinal, calculatedOrdinal),
            Ordinal = calculatedOrdinal,
            Name = name,
            AsvsChapterId = chapterId
        };

        await _context.AsvsSections.AddAsync(section);
        await _context.SaveChangesAsync();

        return new AsvsSectionDetails
        {
            Id = section.Id,
            Code = section.Code.ToVersionString(),
            Ordinal = section.Ordinal,
            Name = section.Name,
            Requirements = new List<AsvsRequirementDetails>().AsReadOnly()
        };
    }

    /// <summary>
    /// Update ASVS section.
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="chapterId">Chapter identifier.</param>
    /// <param name="sectionId">Section identifier.</param>
    /// <param name="name">Section name.</param>
    public async Task UpdateSectionAsync(Guid versionId, Guid chapterId, Guid sectionId, string name)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and chapters cannot be added.");
        }

        var section = await _context.AsvsSections.FirstOrDefaultAsync(s => s.Id.Equals(sectionId) && s.AsvsChapterId.Equals(chapterId));
        if (section is null)
        {
            throw new NotFoundException($"Section '{sectionId} does not exist on the ASVS version.");
        }

        section.Name = name;

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create ASVS requirement
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="chapterId">Chapter identifier.</param>
    /// <param name="sectionId">Section identifier.</param>
    /// <param name="description">Requirement description.</param>
    /// <param name="level">Requirement ASVS level.</param>
    /// <returns>The newly created <see cref="AsvsRequirementDetails"/>.</returns>
    public async Task<AsvsRequirementDetails> CreateRequirementAsync(Guid versionId, Guid chapterId, Guid sectionId, string description, AsvsLevel level)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and requirements cannot be added.");
        }

        var chapter = await _context.AsvsChapters.FirstOrDefaultAsync(c => c.Id.Equals(chapterId) && c.AsvsVersionId.Equals(versionId));
        if (chapter is null)
        {
            throw new NotFoundException($"Chapter '{chapterId}' does not exist on the given ASVS version.");
        }

        var section = await _context.AsvsSections.FirstOrDefaultAsync(s => s.Id.Equals(sectionId) && s.AsvsChapterId.Equals(chapterId));
        if (section is null)
        {
            throw new NotFoundException($"Section '{sectionId}' does not exist on the chapter on the given ASVS version.");
        }

        var currentOrdinal = await _context.AsvsRequirements
            .Where(r => r.AsvsSectionId.Equals(sectionId))
            .MaxAsync(c => (int?)c.Ordinal);

        var calculatedOrdinal = currentOrdinal is null ? 1 : currentOrdinal.Value + 1;

        var requirement = new AsvsRequirement
        {
            Code = new AsvsCode(chapter.Ordinal, section.Ordinal, calculatedOrdinal),
            Ordinal = calculatedOrdinal,
            Description = description,
            Level = level,
            AsvsSectionId = sectionId
        };

        await _context.AsvsRequirements.AddAsync(requirement);
        await _context.SaveChangesAsync();

        return new AsvsRequirementDetails
        {
            Id = requirement.Id,
            Code = requirement.Code.ToVersionString(),
            Ordinal = requirement.Ordinal,
            Description = requirement.Description,
            Level = requirement.Level
        };
    }

    /// <summary>
    /// Update ASVS requirement.
    /// </summary>
    /// <param name="versionId">ASVS version identifier.</param>
    /// <param name="chapterId">Chapter identifier.</param>
    /// <param name="sectionId">Section identifier.</param>
    /// <param name="requirementId">Requirement identifier.</param>
    /// <param name="description">Requirement description.</param>
    /// <param name="level">Requirement ASVS level.</param>
    public async Task UpdateRequirementAsync(Guid versionId, Guid chapterId, Guid sectionId, Guid requirementId, string description, AsvsLevel level)
    {
        var version = await _context.AsvsVersions.FirstOrDefaultAsync(v => v.Id.Equals(versionId));
        if (version is null)
        {
            throw new NotFoundException($"ASVS version '{versionId}' does not exist.");
        }

        if (version.IsReadOnly)
        {
            throw new InvalidOperationException("ASVS version is read only and chapters cannot be added.");
        }

        var sectionExists = await _context.AsvsSections.AnyAsync(s => s.Id.Equals(sectionId) && s.AsvsChapterId.Equals(chapterId));
        if (!sectionExists)
        {
            throw new NotFoundException($"Section '{sectionId}' does not exist on the chapter on the given ASVS version.");
        }

        var requirement = await _context.AsvsRequirements.FirstOrDefaultAsync(r => r.Id.Equals(requirementId) && r.AsvsSectionId.Equals(sectionId));
        if (requirement is null)
        {
            throw new NotFoundException($"Requirement '{requirementId} does not exist on the ASVS version.");
        }

        requirement.Description = description;
        requirement.Level = level;

        await _context.SaveChangesAsync();
    }
}
