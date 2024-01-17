using Microsoft.EntityFrameworkCore;

namespace Authentication.SupportHub.Infrastructure.Contexts.Persistences;

public static class AuthenticationDbContextFactory
{
	public static async Task CreateAsync(string connectionString)
	{
		try
		{
			var optionsBuilder = new DbContextOptionsBuilder<AuthenticationDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			await using var authenticationDbContext = new AuthenticationDbContext(optionsBuilder.Options);
			await authenticationDbContext.Database.EnsureCreatedAsync();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error creating database: {ex.Message}");
			throw;
		}
	}
}