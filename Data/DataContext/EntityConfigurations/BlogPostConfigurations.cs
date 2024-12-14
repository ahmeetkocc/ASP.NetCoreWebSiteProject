using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.DataContext.EntityConfigurations
{
	public class BlogPostConfigurations
	{
		public class BlogPostsConfigurations : IEntityTypeConfiguration<BlogPost>
		{
			public void Configure(EntityTypeBuilder<BlogPost> builder)
			{
				builder.HasKey(bp => bp.Id);

				builder.Property(bp => bp.Title)
					   .IsRequired()
					   .HasMaxLength(100)
					   .HasColumnType("varchar(100)");

				builder.Property(bp => bp.Content)
					   .IsRequired()
					   .HasColumnType("text");

				builder.Property(bp => bp.PublishDate)
					   .IsRequired()
					   .HasColumnType("datetime");

			}
		}
	}
}
