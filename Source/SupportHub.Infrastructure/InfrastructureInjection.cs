using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using SupportHub.Domain.APIs;
using SupportHub.Domain.Cache;
using SupportHub.Domain.Repositories;
using SupportHub.Domain.Services;
using SupportHub.Infrastructure.APIs;
using SupportHub.Infrastructure.Cache;
using SupportHub.Infrastructure.Contexts;
using SupportHub.Infrastructure.Repositories;
using SupportHub.Infrastructure.Services;

namespace SupportHub.Infrastructure;

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

		services.AddScoped<ISessionCache, SessionCache>();
		services.AddScoped<IOneTimePasswordCache, OneTimePasswordCache>();
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(configuration["SqlServer_ConnectionString"], sqlOptions =>
			{
				sqlOptions.MigrationsAssembly("SupportHub.Infrastructure");
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