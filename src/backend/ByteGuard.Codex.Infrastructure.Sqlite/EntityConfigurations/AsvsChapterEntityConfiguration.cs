using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class AsvsChapterEntityConfiguration : IEntityTypeConfiguration<AsvsChapter>
{
    public void Configure(EntityTypeBuilder<AsvsChapter> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Ordinal)
            .IsRequired();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.HasOne(x => x.AsvsVersion)
            .WithMany(x => x.AsvsChapters)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.AsvsSections)
            .WithOne(x => x.AsvsChapter)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
