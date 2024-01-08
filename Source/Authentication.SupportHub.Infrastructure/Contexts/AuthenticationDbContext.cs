using Authentication.SupportHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.SupportHub.Infrastructure.Contexts;

public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
{
	public DbSet<Account>? Accounts { get; set; }
	public DbSet<Employee>? Employees { get; set; }
}