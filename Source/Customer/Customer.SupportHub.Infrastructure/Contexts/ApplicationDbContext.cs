using Microsoft.EntityFrameworkCore;

namespace Customer.SupportHub.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Customer>? Customers { get; set; }
}