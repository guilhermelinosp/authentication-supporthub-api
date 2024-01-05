using Customer.SupportHub.Domain.Entities;
using Customer.SupportHub.Domain.Repositories;
using Customer.SupportHub.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Customer.SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(CustomerDbContext context) : IEmployeeRepository, IInfrastructureInjection
{
	public async Task<Employee?> FindEmployeeByIdAsync(Guid employeeId)
	{
		return await context.Employees!.AsNoTracking().FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
	}

	public async Task<Employee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
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