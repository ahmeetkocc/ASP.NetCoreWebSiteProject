using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.DataEntities;

namespace Data.DataContext.EntityConfigurations
{
	public class EducationsConfigurations : IEntityTypeConfiguration<Education>
	{
		public void Configure(EntityTypeBuilder<Education> builder)
		{
			// Primary Key
			builder.HasKey(e => e.Id);

			// Properties
			builder.Property(e => e.Degree)
				   .IsRequired()
				   .HasMaxLength(50)
				   .HasColumnType("varchar(50)");

			builder.Property(e => e.School)
				   .IsRequired()
				   .HasMaxLength(100)
				   .HasColumnType("varchar(100)");

			builder.Property(e => e.StartDate)
				   .IsRequired()
				   .HasColumnType("date");

			builder.Property(e => e.EndDate)
				   .HasColumnType("date");

			//builder.HasOne(e => e.User)
			//    .WithMany()
			//    .IsRequired()
			//    .HasForeignKey(e => e.UserId)
			//    .OnDelete(DeleteBehavior.NoAction);
		}

	}
}
