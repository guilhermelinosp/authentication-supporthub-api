using System.Reflection;
using Employee.SupportHub.Domain.APIs;
using Employee.SupportHub.Domain.Cache;
using Employee.SupportHub.Domain.Repositories;
using Employee.SupportHub.Domain.Services;
using Employee.SupportHub.Infrastructure.APIs;
using Employee.SupportHub.Infrastructure.Cache;
using Employee.SupportHub.Infrastructure.Contexts;
using Employee.SupportHub.Infrastructure.Repositories;
using Employee.SupportHub.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Employee.SupportHub.Infrastructure;

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
		services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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