using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class AsvsSectionEntityConfiguration : IEntityTypeConfiguration<AsvsSection>
{
    public void Configure(EntityTypeBuilder<AsvsSection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Ordinal)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.AsvsChapter)
            .WithMany(x => x.AsvsSections)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AsvsRequirements)
            .WithOne(x => x.AsvsSection)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
