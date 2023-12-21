using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Auth.Infrastructure;

namespace SupportHub.Authentication.Application;

public interface IApplicationInjection;

public static class ApplicationInjection
{
	public static IServiceCollection AddApplicationInjection(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddInfrastructureInjection(configuration);

		services.Scan(scan =>
			scan.FromAssemblies(ApplicationAssembly.Assembly)
				.AddClasses(filter => filter.AssignableTo<IApplicationInjection>()).AsImplementedInterfaces()
				.WithScopedLifetime());

		return services;
	}

	private static class ApplicationAssembly
	{
		public static readonly Assembly Assembly = typeof(ApplicationAssembly).Assembly;
	}
}