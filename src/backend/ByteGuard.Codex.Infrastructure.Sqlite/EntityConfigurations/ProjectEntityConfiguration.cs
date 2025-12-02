using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(2000);

        builder.Property(x => x.Owner)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.ArchivedAt)
            .IsRequired(false);

        builder.HasOne(x => x.AsvsVersion)
            .WithMany()
            .HasForeignKey(x => x.AsvsVersionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Requirements)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
