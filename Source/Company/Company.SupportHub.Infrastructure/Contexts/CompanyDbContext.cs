using Microsoft.EntityFrameworkCore;

namespace Company.SupportHub.Infrastructure.Contexts;

public class CompanyDbContext(DbContextOptions<CompanyDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Company>? Companies { get; set; }
	public DbSet<Domain.Entities.Employee>? Employees { get; set; }
}