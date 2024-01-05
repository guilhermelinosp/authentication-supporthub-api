using Company.SupportHub.Domain.Entities;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company.SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(CompanyDbContext context) : IEmployeeRepository, IInfrastructureInjection
{
	public async Task<AccountEmployee?> FindEmployeeByIdAsync(Guid employeeId)
	{
		return await context.Employees!.AsNoTracking().FirstOrDefaultAsync(u => u.EmployeeId == employeeId);
	}

	public async Task<AccountEmployee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
	}

	public async Task UpdateEmployeeAsync(AccountEmployee customer)
	{
		context.Employees!.Update(customer);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}