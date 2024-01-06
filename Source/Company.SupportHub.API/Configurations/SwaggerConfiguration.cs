using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Company.SupportHub.API.Configurations;

public static class SwaggerConfiguration
{
	public static void AddSwaggerConfiguration(this IServiceCollection services)
	{
		services.AddEndpointsApiExplorer();
		services.AddHttpContextAccessor();
		services.AddSwaggerGen(opt =>
		{
			opt.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Company Support Hub API - Authentication",
				Version = "v1",
				Description = "Welcome to the Company Support Hub API...",
				Contact = new OpenApiContact
				{
					Name = "Support Hub",
					Email = "guilhermelinosp@gmail.com"
				},
				License = new OpenApiLicense
				{
					Name = "MIT",
					Url = new Uri("https://opensource.org/licenses/MIT")
				}
			});

			opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				In = ParameterLocation.Header,
				Description = "JWT Authorization header using the Bearer scheme."
			});

			opt.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					Array.Empty<string>()
				}
			});
		});
	}
}