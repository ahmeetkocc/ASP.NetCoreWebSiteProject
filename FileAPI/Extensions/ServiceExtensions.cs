namespace FileAPI.Extensions;

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
					builder.AllowAnyMethod() // Tüm HTTP metodlarına izin ver
						   .AllowAnyHeader() // Tüm headerlara izin ver
						   .AllowCredentials(); // Credential isteklerine izin ver
				});
		});

		return services;
	}
}
