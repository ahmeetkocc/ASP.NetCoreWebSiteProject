using Data.AuthEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.AuthContext.EntityConfigurations
{
	public class RoleConfigurations : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(r => r.Id);
			builder.Property(r => r.Id).ValueGeneratedOnAdd();

			builder.Property(u => u.Name)
				.HasMaxLength(50)
				.IsRequired();

			builder.HasData(
				new Role { Id = 1, Name = "admin" },
				new Role { Id = 2, Name = "commenter" });
		}
	}
}
