using Microsoft.EntityFrameworkCore;

namespace Authentication.SupportHub.Infrastructure.Contexts.Persistences;

public static class AuthenticationDbContextFactory
{
	public static async Task Create(string connectionString)
	{
		var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
		optionsBuilder.UseSqlServer(connectionString);
		var attractionDbContext = new AuthenticationDbContext(optionsBuilder.Options);
		await attractionDbContext.Database.EnsureCreatedAsync();
	}
}