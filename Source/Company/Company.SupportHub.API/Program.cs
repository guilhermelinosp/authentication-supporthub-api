using Company.SupportHub.API.Configurations;
using Company.SupportHub.API.Filters;
using Company.SupportHub.Application;

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
	configuration.AddUserSecrets<Program>();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Any");
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();