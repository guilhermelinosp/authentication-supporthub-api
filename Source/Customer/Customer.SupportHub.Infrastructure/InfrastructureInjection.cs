using System.Reflection;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;
using Customer.SupportHub.Infrastructure.Contexts;
using Customer.SupportHub.Infrastructure.Repositories;
using Customer.SupportHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Customer.SupportHub.Infrastructure;

public interface IInfrastructureInjection;

public static class InfrastructureInjection
{
	public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContexts(configuration);
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
			options.Configuration = configuration["Redis_ConnectionString"];
		});

		services.AddSingleton<IConnectionMultiplexer>(options =>
			ConnectionMultiplexer.Connect(configuration["Redis_ConnectionString"]!));
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContextPool<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"]));
	}

	private static class InfrastructureAssembly
	{
		public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
	}
}