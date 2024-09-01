using AuthAPI.Data.EntityConfigurations;
using AuthAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Data
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
