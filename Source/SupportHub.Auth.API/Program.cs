using SupportHub.Auth.API.Configuration;
using SupportHub.Auth.Application;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

services.AddPresentation()
    .AddAplication()
    .AddApplicationInjection(configuration)
    .AddAuthentication(configuration);

services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = false;
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    configuration.AddUserSecrets<Program>();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("localhost");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();