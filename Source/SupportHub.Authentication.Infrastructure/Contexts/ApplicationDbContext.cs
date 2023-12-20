using Microsoft.EntityFrameworkCore;
using SupportHub.Authentication.Domain.Entities;

namespace SupportHub.Auth.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Company>? Companies { get; set; }
	public DbSet<Employee>? Employees { get; set; }
	public DbSet<Customer>? Customers { get; set; }
}