using ByteGuard.Codex.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ByteGuard.Codex.Infrastructure.Sqlite.EntityConfigurations;

internal class CustomRequirementEntityConfiguration : IEntityTypeConfiguration<CustomRequirement>
{
    public void Configure(EntityTypeBuilder<CustomRequirement> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1500);

        builder.Property(x => x.Level)
            .IsRequired();
    }
}
