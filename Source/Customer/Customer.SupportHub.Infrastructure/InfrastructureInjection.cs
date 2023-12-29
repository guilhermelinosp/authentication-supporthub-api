using Customer.SupportHub.Domain.APIs;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;
using Customer.SupportHub.Infrastructure.APIs;
using Customer.SupportHub.Infrastructure.Contexts;
using Customer.SupportHub.Infrastructure.Repositories;
using Customer.SupportHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Customer.SupportHub.Infrastructure;

public static class InfrastructureInjection
{
	public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClients(configuration);
		services.AddDbContexts(configuration);
		services.AddCaches(configuration);

		services.Scan(scan =>
			scan.FromAssemblies(InfrastructureAssembly.Assembly)
				.AddClasses(filter => filter.AssignableTo<IInfrastructureInjection>()).AsImplementedInterfaces()
				.WithScopedLifetime());
	}

	private static void AddCaches(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["Redis_ConnectionString"]!));
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<CustomerDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"], sqlServerOptions =>
			{
				sqlServerOptions.EnableRetryOnFailure();
			}));
	}

	private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IBrazilApi, BrazilApi>(options =>
		{
			options.BaseAddress = new Uri(configuration["BrazilApi_Url"]!);
		});
	}
}