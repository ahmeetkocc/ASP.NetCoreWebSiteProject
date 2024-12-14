using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.DataContext
{
	public class AppDbContext : DbContext
	{
		public DbSet<AboutMe> AboutMe { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<ContactMessage> ContactMessages { get; set; }
		public DbSet<Education> Educations { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<PersonalInfo> PersonalInfos { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Skill> Skills { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
