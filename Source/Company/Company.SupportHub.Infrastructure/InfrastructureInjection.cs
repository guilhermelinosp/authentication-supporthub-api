using System.Reflection;
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
			options.Configuration = configuration["Redis_ConnectionString"];
		});

		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["Redis_ConnectionString"]!));
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"], sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("Company.SupportHub.Infrastructure");
				sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
			}));
	}

	private static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IBrazilApi, BrazilApi>(options =>
		{
			options.BaseAddress = new Uri(configuration["BrazilApi_Url"]!);
		});
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<ICompanyRepository, CompanyRepository>();
	}

	private static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<ISendGridService, SendGridService>();
		services.AddScoped<ITwilioService, TwilioService>();
		services.AddScoped<IRedisService, RedisService>();
	}
}
public static class InfrastructureAssembly
{
	public static readonly Assembly Assembly = typeof(InfrastructureAssembly).Assembly;
}