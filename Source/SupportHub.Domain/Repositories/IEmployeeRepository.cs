using SupportHub.Domain.Entities;

namespace SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<Employee?> FindEmployeeByIdAsync(Guid employeeid);
	Task<Employee?> FindEmployeeByEmailAsync(string email);
	Task UpdateEmployeeAsync(Employee customer);
}