using Company.SupportHub.Domain.Entities;

namespace Company.SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<AccountEmployee?> FindEmployeeByIdAsync(Guid employeeid);
	Task<AccountEmployee?> FindEmployeeByEmailAsync(string email);
	Task UpdateEmployeeAsync(AccountEmployee customer);
}