using DataAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data.EntityConfigurations
{
    public class ExperiencesConfigurations : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.Company)
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(e => e.StartDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.EndDate)
                .HasColumnType("date")
                .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("text")
                .IsRequired(false);
        }
    }
}
