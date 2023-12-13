using SupportHub.Auth.API.Configurations;
using SupportHub.Auth.API.Filters;
using SupportHub.Auth.Application;

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
services.AddControllers(options =>
{
    options.Filters.AddService<ExceptionFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    configuration.AddUserSecrets<Program>();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("Any");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();