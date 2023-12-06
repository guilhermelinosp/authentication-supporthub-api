using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.ServicesExternal;
using SupportHub.Auth.Infrastructure.Contexts;
using SupportHub.Auth.Infrastructure.Repositories;

namespace SupportHub.Auth.Infrastructure;

public static class InfrastructureInjection
{
    public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
    {
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
}