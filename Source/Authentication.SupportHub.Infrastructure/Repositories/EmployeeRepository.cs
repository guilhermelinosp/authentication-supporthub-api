using Authentication.SupportHub.Domain.Entities;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Authentication.SupportHub.Infrastructure.Repositories;

public class EmployeeRepository(AuthenticationDbContext context) : IEmployeeRepository, IInfrastructureInjection
{
	public async Task<Employee?> FindEmployeeByEmailAsync(string email)
	{
		return await context.Employees!.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
	}
}