using Microsoft.EntityFrameworkCore;

namespace Employee.SupportHub.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Employee>? Employees { get; set; }
}