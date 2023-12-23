using System.Reflection;
using Customer.SupportHub.Domain.APIs;
using Customer.SupportHub.Domain.Cache;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Domain.Services;
using Customer.SupportHub.Infrastructure.APIs;
using Customer.SupportHub.Infrastructure.Cache;
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

		services.AddScoped<ISessionCache, SessionCache>();
		services.AddScoped<IOneTimePasswordCache, OneTimePasswordCache>();
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"], sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("CompanyEntity.SupportHub.Infrastructure");
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