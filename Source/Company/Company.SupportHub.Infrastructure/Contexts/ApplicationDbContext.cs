using Microsoft.EntityFrameworkCore;
using Company.SupportHub.Domain.Entities;

namespace Company.SupportHub.Infrastructure.Contexts;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Domain.Entities.Company>? Companies { get; set; }
}