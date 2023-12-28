using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Company.SupportHub.Domain.APIs;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Domain.Services;
using Company.SupportHub.Infrastructure.APIs;
using Company.SupportHub.Infrastructure.Contexts;
using Company.SupportHub.Infrastructure.Repositories;
using Company.SupportHub.Infrastructure.Services;

namespace Company.SupportHub.Infrastructure;

public static class InfrastructureInjection
{
	public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient(configuration);
		services.AddDbContext(configuration);
		services.AddRedis(configuration);

		services.Scan(scan =>
			scan.FromAssemblies(InfrastructureAssembly.Assembly)
				.AddClasses(filter => filter.AssignableTo<IInfrastructureInjection>()).AsImplementedInterfaces()
				.WithScopedLifetime());
	}

	private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddStackExchangeRedisCache(options =>
		{
			options.Configuration = configuration["Redis_ConnectionString"];
		});

		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["Redis_ConnectionString"]!));
	}

	private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<InfrastructureDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"]));
	}

	private static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IBrazilApi, BrazilApi>(options =>
		{
			options.BaseAddress = new Uri(configuration["BrazilApi_Url"]!);
		});
	}
}