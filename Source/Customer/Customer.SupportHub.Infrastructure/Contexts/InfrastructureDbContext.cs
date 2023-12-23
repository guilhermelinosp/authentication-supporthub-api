using Microsoft.EntityFrameworkCore;

namespace Customer.SupportHub.Infrastructure.Contexts;

public class InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Customer>? Customers { get; set; }
	public DbSet<Domain.Entities.Employee>? Employees { get; set; }
}