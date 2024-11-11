using DataAPI.Services.Implementations;
using DataAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAPI.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		// Add Controllers
		services.AddControllers();

		// Add Swagger
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen();

		// Add CORS
		services.AddCors(options =>
		{
			options.AddPolicy("AllowSpecificOrigin",
				builder =>
				{
					builder.AllowAnyMethod()
						   .AllowAnyHeader()
						   .AllowCredentials();
				});
		});

		// Add Services
		services.AddScoped<IAboutMeService, AboutMeService>();
		services.AddScoped<IBlogPostService, BlogPostService>();
		services.AddScoped<ICommentService, CommentService>();
		services.AddScoped<IContactMessageService, ContactMessageService>();
		services.AddScoped<IEducationService, EducationService>();
		services.AddScoped<IExperienceService, ExperienceService>();
		services.AddScoped<IPersonalInfoService, PersonalInfoService>();
		services.AddScoped<IProjectService, ProjectService>();
		services.AddScoped<ISkillService, SkillService>();

		return services;
	}
}
