using SupportHub.Auth.API.Filters;

namespace SupportHub.Auth.API.Configuration;

public static class PresentationConfiguration
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers(options => { options.Filters.AddService<ValidationFilter>(); });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerDocumentation();

        return services;
    }
}