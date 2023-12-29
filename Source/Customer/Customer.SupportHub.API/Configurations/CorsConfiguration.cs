namespace Customer.SupportHub.API.Configurations;

public static class CorsConfiguration
{
	public static void AddCorsConfiguration(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddDefaultPolicy(builder =>
			{
				builder
					.AllowAnyHeader()
					.AllowCredentials()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});
	}
}