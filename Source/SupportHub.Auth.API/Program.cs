using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SupportHub.Auth.API.Filters;
using SupportHub.Auth.Application;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var services = builder.Services;

services.AddScoped<ValidationFilter>();
services.AddApplicationInjection(configuration);
services.AddEndpointsApiExplorer();
services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>();
});

services.AddSwaggerGen();

services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = false;
});

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true
    };
});

services.AddCors(options =>
{
    options.AddPolicy("localhost", opt =>
    {
        opt.WithOrigins("http://127.0.0.1")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    configuration.AddUserSecrets<Program>();
}

app.UseRouting();
app.UseCors("localhost");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();