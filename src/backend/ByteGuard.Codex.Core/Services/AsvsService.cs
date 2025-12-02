using ByteGuard.Codex.Core.Abstractions.DataStorage;
using ByteGuard.Codex.Core.Entities;
using ByteGuard.Codex.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteGuard.Codex.Core.Services;

public class AsvsService
{
    private readonly ICodexDbContext _context;

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
                Description = v.Description
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
                Chapters = v.AsvsChapters.Select(c => new AsvsChapterDetails
                {
                    Id = c.Id,
                    Code = c.Code,
                    Ordinal = c.Ordinal,
                    Title = c.Title,
                    Description = c.Description,
                    Sections = c.AsvsSections.Select(s => new AsvsSectionDetails
                    {
                        Id = s.Id,
                        Code = s.Code,
                        Ordinal = s.Ordinal,
                        Name = s.Name,
                        Requirements = s.AsvsRequirements.Select(r => new AsvsRequirementDetails
                        {
                            Id = r.Id,
                            Code = r.Code,
                            Ordinal = r.Ordinal,
                            Description = r.Description,
                            Level = r.Level
                        }).OrderBy(r => r.Ordinal).ToList().AsReadOnly()
                    }).OrderBy(s => s.Ordinal).ToList().AsReadOnly()
                }).OrderBy(c => c.Ordinal).ToList().AsReadOnly()
            }).FirstOrDefaultAsync(x => x.Id.Equals(id));

        return version;
    }
}
