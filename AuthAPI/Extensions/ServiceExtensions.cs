using Data.AuthContext;
using AuthAPI.Services.Implementations;
using AuthAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AuthAPI.Extensions;

public static class ServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Add Controllers
		services.AddControllers();

		// Add Swagger
		services.AddEndpointsApiExplorer();
		services.AddSwaggerGen(c =>
		{
			c.AddSecurityDefinition(
				name: "Bearer",
				securityScheme: new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description =
						"JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345.54321\""
				}
			);
			c.AddSecurityRequirement(
				new OpenApiSecurityRequirement
				{
						{
							new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
							},
							new string[] { }
						}
				}
			);
		});

		// Add Services
		services.AddScoped<IAuthService, AuthService>();

		// Add Authentication
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
				};
				options.MapInboundClaims = true;
			});

		// Add DbContext
		services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AuthApiConStr")));

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

		return services;
	}
}
