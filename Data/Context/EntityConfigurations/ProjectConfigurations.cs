using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.Context.EntityConfigurations;

public class ProjectsConfigurations : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnType("text")
            .IsRequired(false);

        builder.Property(a => a.ImageUrl)
            .HasMaxLength(255)
            .HasColumnType("varchar(255)");     
    }
}
