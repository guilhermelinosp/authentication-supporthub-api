using SupportHub.Domain.Entities;

namespace SupportHub.Domain.Repositories;

public interface ICustomerRepository
{
	Task<Customer?> FindCustomerByIdAsync(Guid customerid);
	Task<Customer?> FindCustomerByEmailAsync(string email);
	Task UpdateCustomerAsync(Customer customer);
}