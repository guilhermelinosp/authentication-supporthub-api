namespace Employee.SupportHub.Domain.Repositories;

public interface IEmployeeRepository
{
	Task<Entities.Employee?> FindEmployeeByIdAsync(Guid employeeid);
	Task<Entities.Employee?> FindEmployeeByEmailAsync(string email);
	Task UpdateEmployeeAsync(Entities.Employee customer);
}