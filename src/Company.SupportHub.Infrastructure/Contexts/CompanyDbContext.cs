using Microsoft.EntityFrameworkCore;

namespace Company.SupportHub.Infrastructure.Contexts;

public class CompanyDbContext(DbContextOptions<CompanyDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.AccountCompany>? Companies { get; set; }
	public DbSet<Domain.Entities.AccountEmployee>? Employees { get; set; }
}