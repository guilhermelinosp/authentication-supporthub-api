using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Authentication.SupportHub.Domain.APIs;
using Authentication.SupportHub.Infrastructure.APIs;
using Authentication.SupportHub.Infrastructure.Contexts;

namespace Authentication.SupportHub.Infrastructure;

public interface IInfrastructureInjection;

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
		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]!));
	}

	private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AuthenticationDbContext>(options =>
			options.UseSqlServer(configuration["ConnectionStrings:SqlServer"],
				sqlServerOptions => { sqlServerOptions.EnableRetryOnFailure(); }));
	}

	private static void AddHttpClient(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IBrazilApi, BrazilApi>(options =>
		{
			options.BaseAddress = new Uri(configuration["BrazilApi:Url"]!);
		});
	}

	private static class InfrastructureAssembly
	{
		public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
	}
}