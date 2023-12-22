using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SupportHub.Infrastructure;

namespace SupportHub.Application;

public interface IApplicationInjection;

public static class ApplicationInjection
{
	public static void AddApplicationInjection(this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddInfrastructureInjection(configuration);

		services.Scan(scan =>
			scan.FromAssemblies(ApplicationAssembly.Assembly)
				.AddClasses(filter => filter.AssignableTo<IApplicationInjection>()).AsImplementedInterfaces()
				.WithScopedLifetime());
	}

	private static class ApplicationAssembly
	{
		public static readonly Assembly Assembly = typeof(ApplicationAssembly).Assembly;
	}
}