using SupportHub.API.Configurations;
using SupportHub.API.Filters;
using SupportHub.Authentication.Application;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddApplicationInjection(configuration);

services.AddAuthorizationConfiguration(configuration);
services.AddAuthenticationConfiguration(configuration);
services.AddSwaggerConfiguration(configuration);
services.AddRoutingConfiguration(configuration);
services.AddCorsConfiguration(configuration);

services.AddScoped<ExceptionFilter>();
services.AddControllers(options => { options.Filters.AddService<ExceptionFilter>(); });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	configuration.AddUserSecrets<Program>();
}

if (app.Environment.IsProduction())
{
	app.UseHsts();
}

app.UseCors("Any");
app.UseExceptionHandler("/error");
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();