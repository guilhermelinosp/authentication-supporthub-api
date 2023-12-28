using Company.SupportHub.Domain.Entities;
using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<Employee?> FindEmployeeByIdAsync(Guid employeeid);
	Task<Employee?> FindEmployeeByEmailAsync(string email);
	Task UpdateEmployeeAsync(Employee customer);
}