using System.Reflection;
using Employee.SupportHub.Domain.Repositories;
using Employee.SupportHub.Domain.Services;
using Employee.SupportHub.Infrastructure.Contexts;
using Employee.SupportHub.Infrastructure.Repositories;
using Employee.SupportHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Employee.SupportHub.Infrastructure;

public interface IInfrastructureInjection;

public static class InfrastructureInjection
{
	public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
	{
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
			options.Configuration = configuration["Redis_ConnectionString"];
		});

		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["Redis_ConnectionString"]!));
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContextPool<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"]));
	}


	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
	}

	private static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<ISendGridService, SendGridService>();
		services.AddScoped<IRedisService, RedisService>();
	}
}

public static class InfrastructureAssembly
{
	public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
}