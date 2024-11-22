using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.DataEntities;

namespace Data.DataContext.EntityConfigurations
{
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

			// Foreign key relationships
			builder.HasOne(c => c.BlogPost)
				   .WithMany(c => c.Comments)  // Assuming BlogPosts can have multiple Comments
				   .HasForeignKey(c => c.BlogPostId)
				   .OnDelete(DeleteBehavior.Cascade);

			//builder.HasOne(e => e.User)
			//    .WithMany()
			//    .IsRequired()
			//    .HasForeignKey(e => e.UserId)
			//    .OnDelete(DeleteBehavior.NoAction);
		}
	}
}
