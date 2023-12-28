using Company.SupportHub.Domain.Entities;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company.SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(InfrastructureDbContext context) : IEmployeeRepository, IInfrastructureInjection
{
	public async Task<Employee?> FindEmployeeByIdAsync(Guid employeeId)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.EmployeeId == employeeId);
	}

	public async Task<Employee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task UpdateEmployeeAsync(Employee customer)
	{
		context.Employees!.Update(customer);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}