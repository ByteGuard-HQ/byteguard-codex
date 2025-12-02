using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class AsvsVersionEntityConfiguration : IEntityTypeConfiguration<AsvsVersion>
{
    public void Configure(EntityTypeBuilder<AsvsVersion> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.VersionNumber)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.HasMany(x => x.AsvsChapters)
            .WithOne(x => x.AsvsVersion)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
