﻿namespace Company.SupportHub.API.Configurations;

public static class CorsConfiguration
{
	public static void AddCorsConfiguration(this IServiceCollection services)
	{
		services.AddCors(options =>
		{
			options.AddPolicy("Any", builder =>
			{
				builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
		});
	}
}