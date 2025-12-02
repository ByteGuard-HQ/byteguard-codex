using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class AsvsRequirementEntityConfiguration : IEntityTypeConfiguration<AsvsRequirement>
{
    public void Configure(EntityTypeBuilder<AsvsRequirement> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Ordinal)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1500);

        builder.Property(x => x.Level)
            .IsRequired();

        builder.HasOne(x => x.AsvsSection)
            .WithMany(x => x.AsvsRequirements)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
