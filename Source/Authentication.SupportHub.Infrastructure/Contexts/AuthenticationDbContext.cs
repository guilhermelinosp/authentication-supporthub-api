using Authentication.SupportHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authentication.SupportHub.Infrastructure.Contexts
{
	public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
	{
		public DbSet<Account>? Accounts { get; set; }
		public DbSet<Company>? Companies { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthenticationDbContext).Assembly);
		}
	}
}