using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Auth.Domain.Apis;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.ServicesExternal;
using SupportHub.Auth.Infrastructure.Apis;
using SupportHub.Auth.Infrastructure.Contexts;
using SupportHub.Auth.Infrastructure.Repositories;

namespace SupportHub.Auth.Infrastructure;

public static class InfrastructureInjection
{
    public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        AddBrasilApi(services, configuration);
        services.AddRepositories();
        services.AddServicesExternal();
        services.AddDbContexts(configuration);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }

    private static void AddServicesExternal(this IServiceCollection services)
    {
        services.AddScoped<ISendGrid, ServicesExternal.SendGrid>();
        services.AddScoped<ITwilio, ServicesExternal.Twilio>();
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SupportHubContext>(options =>
            options.UseSqlServer(configuration["SqlServer:ConnectionString"]));
    }

    private static void AddBrasilApi(IServiceCollection services, IConfiguration configuration)
    {
        var configUrl = configuration.GetValue<string>("BrasilApi:Url");
        services.AddHttpClient<IBrasilApi, BrasilApi>(options => { options.BaseAddress = new Uri($"{configUrl}"); });
    }
}