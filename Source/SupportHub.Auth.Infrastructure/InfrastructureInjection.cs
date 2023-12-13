using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SupportHub.Auth.Domain.APIs;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.Services;
using SupportHub.Auth.Infrastructure.APIs;
using SupportHub.Auth.Infrastructure.Cache;
using SupportHub.Auth.Infrastructure.Contexts;
using SupportHub.Auth.Infrastructure.Repositories;
using SupportHub.Auth.Infrastructure.Services;

namespace SupportHub.Auth.Infrastructure;

public interface IInfrastructureInjection;

public static class InfrastructureInjection
{
    public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClients(configuration);
        services.AddDbContexts(configuration);
        services.AddRepositories();
        services.AddServices();
        services.AddCaches(configuration);
        
        services.Scan(scan =>
            scan.FromAssemblies(InfrastructureAssembly.Assembly)
                .AddClasses(filter => filter.AssignableTo<IInfrastructureInjection>()).AsImplementedInterfaces()
                .WithScopedLifetime());
    }
    
    private static void AddCaches(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:ConnectionString"];
        });
        
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(configuration["Redis:ConnectionString"]!));
        
        services.AddScoped<ISessionCache, SessionCache>();
        services.AddScoped<IOneTimePasswordCache, OneTimePasswordCache>();
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration["SqlServer:ConnectionString"]));
    }

    private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IBrazilApi, BrazilApi>(options => 
            { options.BaseAddress = new Uri(configuration["BrazilApi:BaseAddress"]!); });
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }
    
    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISendGridService, SendGridServiceService>();
        services.AddScoped<ITwilioService, TwilioServiceService>();
    }
    
    private static class InfrastructureAssembly
    {
        public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
    }
}