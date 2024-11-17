using Data.AuthEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.AuthContext.EntityConfigurations
{
	public class UserConfigurations : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(u => u.Id).ValueGeneratedOnAdd();

			builder.Property(u => u.Username)
				.HasMaxLength(50)
				.IsRequired();

			builder.Property(u => u.PasswordHash)
				.HasMaxLength(255)
				.IsRequired();

			builder.HasOne(u => u.Role)
				.WithMany()
				.HasForeignKey(u => u.RoleId)
				.IsRequired()
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
