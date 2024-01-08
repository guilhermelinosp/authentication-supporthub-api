using Authentication.SupportHub.Domain.Entities;

namespace Authentication.SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<Employee?> FindEmployeeByEmailAsync(string email);
}