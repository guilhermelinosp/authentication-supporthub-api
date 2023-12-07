using Microsoft.EntityFrameworkCore;
using SupportHub.Auth.Domain.Entities;

namespace SupportHub.Auth.Infrastructure.Contexts;

public class SupportHubContext : DbContext
{
    public SupportHubContext(DbContextOptions<SupportHubContext> options) : base(options)
    {
    }

    public DbSet<Company>? Companies { get; set; }
    public DbSet<Employee>? Employees { get; set; }
    public DbSet<Customer>? Customers { get; set; }
}