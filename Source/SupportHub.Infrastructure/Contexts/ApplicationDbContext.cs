using Microsoft.EntityFrameworkCore;
using SupportHub.Domain.Entities;

namespace SupportHub.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Company>? Companies { get; set; }
	public DbSet<Employee>? Employees { get; set; }
	public DbSet<Customer>? Customers { get; set; }
}