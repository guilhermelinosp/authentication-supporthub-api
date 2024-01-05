using Microsoft.EntityFrameworkCore;

namespace Customer.SupportHub.Infrastructure.Contexts;

public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Customer>? Customers { get; set; }
	public DbSet<Domain.Entities.Employee>? Employees { get; set; }
}