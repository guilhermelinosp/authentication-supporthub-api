using Microsoft.OpenApi.Models;

namespace SupportHub.Auth.API.Configuration;

public static class SwaggerConfiguration
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Auth Manage Support",
                Version = "V1",
                Description = "API for handling client authentication.",
                Contact = new OpenApiContact { Name = "Projetin" }
            });
        });
    }
}