using DataAPI.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAPI.Data.EntityConfigurations
{
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

            //builder.HasOne(e => e.User)
            //        .WithMany()
            //        .IsRequired()
            //        .HasForeignKey(e => e.UserId)
            //        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
