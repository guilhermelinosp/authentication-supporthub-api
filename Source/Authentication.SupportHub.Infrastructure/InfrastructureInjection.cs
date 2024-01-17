using Authentication.SupportHub.Domain.APIs;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Domain.Services;
using Authentication.SupportHub.Infrastructure.APIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Authentication.SupportHub.Infrastructure.Contexts;
using Authentication.SupportHub.Infrastructure.Repositories;
using Authentication.SupportHub.Infrastructure.Services;

namespace Authentication.SupportHub.Infrastructure;

public static class InfrastructureInjection
{
	public static void AddInfrastructureInjection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddApis(configuration);
		services.AddDbContexts(configuration);
		services.AddServices(configuration);
		services.AddRepositories();
	}

	private static void AddServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IConnectionMultiplexer>(_ =>
			ConnectionMultiplexer.Connect(configuration["ConnectionStrings:Redis"]!));

		services.AddScoped<IRedisService, RedisService>();
		services.AddScoped<ITwilioService, TwilioService>();
		services.AddScoped<ISendGridService, SendGridService>();
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IAccountRepository, AccountRepository>();
	}

	private static void AddApis(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IBrazilAPI, BrazilApi>(options =>
		{
			options.BaseAddress = new Uri(configuration["BrazilApi:Url"]!);
		});
	}

	private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<AuthenticationDbContext>(options =>
			options.UseSqlServer(configuration["ConnectionStrings:SqlServer"],
				sqlServerOptions => { sqlServerOptions.EnableRetryOnFailure(); }));
	}
}