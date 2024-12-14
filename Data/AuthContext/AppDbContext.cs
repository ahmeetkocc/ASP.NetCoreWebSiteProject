using Data.AuthContext.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data.AuthContext
{
	public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		public AppDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfigurations());
			modelBuilder.ApplyConfiguration(new RoleConfigurations());
			modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
		}
	}


}
