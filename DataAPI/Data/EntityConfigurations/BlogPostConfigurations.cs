using DataAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data.EntityConfigurations
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
