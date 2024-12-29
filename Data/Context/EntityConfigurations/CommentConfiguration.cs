using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.Context.EntityConfigurations;

public class CommentsConfigurations : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
               .IsRequired()
               .HasColumnType("text");

        builder.Property(c => c.CreatedDate)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(c => c.IsApproved)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(c => c.BlogPostId)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(c => c.UserId)
               .IsRequired()
               .HasColumnType("int");

        builder.HasOne(c => c.BlogPost)
               .WithMany(c => c.Comments)
               .HasForeignKey(c => c.BlogPostId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
