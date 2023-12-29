using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Customer.SupportHub.API.Configurations;

public static class AuthenticationConfiguration
{
	public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthorization();
		services.AddAuthentication()
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt_Secret"]!)),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true
				};

				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
			})
			.AddCookie(options =>
			{
				options.Cookie.Name = "RefreshToken";
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
			});
	}
}