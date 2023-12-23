using Employee.SupportHub.Domain.Repositories;
using Employee.SupportHub.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Employee.SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
	public async Task<Domain.Entities.Employee?> FindEmployeeByIdAsync(Guid employeeid)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.EmployeeId == employeeid);
	}

	public async Task<Domain.Entities.Employee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task UpdateEmployeeAsync(Domain.Entities.Employee employee)
	{
		context.Employees!.Update(employee);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}