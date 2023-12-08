using SupportHub.Auth.API.Filters;

namespace SupportHub.Auth.API.Configuration;

public static class ApplicationConfiguration
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        services.AddScoped<ValidationFilter>();
        services.AddEndpointsApiExplorer();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = false;
        });
        services.AddCors(options =>
        {
            options.AddPolicy("localhost", opt =>
            {
                opt.WithOrigins("http://127.0.0.1")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}