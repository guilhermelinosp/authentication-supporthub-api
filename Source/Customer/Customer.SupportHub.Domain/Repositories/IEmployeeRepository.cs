using Customer.SupportHub.Domain.Entities;

namespace Customer.SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<Employee?> FindEmployeeByIdAsync(Guid employeeid);
	Task<Employee?> FindEmployeeByEmailAsync(string email);
	Task UpdateEmployeeAsync(Employee customer);
}