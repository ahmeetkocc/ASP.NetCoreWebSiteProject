using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.DataContext.EntityConfigurations;

public class AboutMeConfigurations : IEntityTypeConfiguration<AboutMe>
{
	public void Configure(EntityTypeBuilder<AboutMe> builder)
	{
		//primary key
		builder.HasKey(a => a.Id);

		// Properties
		builder.Property(a => a.Introduction)
			   .IsRequired()
			   .HasColumnType("text");

		builder.Property(a => a.ImageUrl1)
			   .HasMaxLength(255)
			   .HasColumnType("varchar(255)");

		builder.Property(a => a.ImageUrl2)
			   .HasMaxLength(255)
			   .HasColumnType("varchar(255)");

		builder.HasData(
			new AboutMe
			{
				Id = 1,
				Introduction = "This is a brief introduction about me. I am a software developer with a passion for creating impactful software.",
				ImageUrl1 = "https://picsum.photos/200/300",
				ImageUrl2 = "https://picsum.photos/200/300"
			}
		);

	}
}
