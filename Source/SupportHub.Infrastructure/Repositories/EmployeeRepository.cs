using Microsoft.EntityFrameworkCore;
using SupportHub.Domain.Entities;
using SupportHub.Domain.Repositories;
using SupportHub.Infrastructure.Contexts;

namespace SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
	public async Task<Employee?> FindEmployeeByIdAsync(Guid employeeid)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.EmployeeId == employeeid);
	}

	public async Task<Employee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task UpdateEmployeeAsync(Employee employee)
	{
		context.Employees!.Update(employee);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}