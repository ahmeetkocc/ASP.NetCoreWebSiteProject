using DataAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data.EntityConfigurations
{
    public class PersonalInfoConfigurations : IEntityTypeConfiguration<PersonalInfo>
    {
        public void Configure(EntityTypeBuilder<PersonalInfo> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.About)
                .HasColumnType("varchar(300)")
                .IsRequired(false);  // Nullable

            builder.Property(p => p.Name)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.Surname)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(p => p.BirthDate)
                .HasColumnType("date")
                .IsRequired();

        }
    }
}
