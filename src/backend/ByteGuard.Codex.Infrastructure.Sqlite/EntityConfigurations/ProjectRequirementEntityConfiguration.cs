using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class ProjectRequirementEntityConfiguration : IEntityTypeConfiguration<ProjectRequirement>
{
    public void Configure(EntityTypeBuilder<ProjectRequirement> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Notes)
            .IsRequired(false)
            .HasMaxLength(5000);

        builder.Property(x => x.EvidenceLink)
            .IsRequired(false)
            .HasMaxLength(1500);

        builder.Property(x => x.LastUpdatedAt)
            .IsRequired()
            .ValueGeneratedOnAddOrUpdate();
    }
}
