using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SupportHub.Auth.API.Configurations;

public static class AuthorizationConfiguration
{
	public static void AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthorization(options =>
		{
			options.AddPolicy("CompanyPolicy", policy =>
				policy.RequireRole("Company"));
		});
	}
}