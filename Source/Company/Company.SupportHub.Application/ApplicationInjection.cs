﻿using System.Reflection;
using Company.SupportHub.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Company.SupportHub.Application;

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