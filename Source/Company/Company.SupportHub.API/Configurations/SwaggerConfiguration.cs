using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Company.SupportHub.API.Configurations
{
	public static class SwaggerConfiguration
	{
		public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddEndpointsApiExplorer();
			services.AddHttpContextAccessor();

			services.AddSwaggerGen(opt =>
			{
				ConfigureApiInfo(opt);

				ConfigureSecurityDefinitions(opt);
			});
		}

		private static void ConfigureApiInfo(SwaggerGenOptions opt)
		{
			opt.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "Company Support Hub API - Authentication",
				Version = "V1",
				Description =
					"Welcome to the Company Support Hub API, your gateway to seamless client authentication. This API empowers developers with secure and efficient tools for managing user authentication processes. Dive into a world of cutting-edge authentication solutions designed to elevate your projects. Explore our documentation to harness the full potential of the Company Support Hub API.",
				Contact = new OpenApiContact { Name = "Support Hub", Email = "guilhermelinosp@gmail.com" },
				License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") },
				TermsOfService = new Uri("https://your-terms-of-service-url.com"),
				Extensions = new Dictionary<string, IOpenApiExtension>
				{
					{
						"x-logo", new OpenApiObject
						{
							{ "url", new OpenApiString("https://i.imgur.com/8QZqQ8F.png") },
							{ "altText", new OpenApiString("Company Support Hub API") }
						}
					}
				}
			});
		}

		private static void ConfigureSecurityDefinitions(SwaggerGenOptions opt)
		{
			opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer",
				In = ParameterLocation.Header,
				Description =
					"JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
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
		}
	}
}