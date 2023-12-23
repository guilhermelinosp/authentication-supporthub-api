namespace Customer.SupportHub.Domain.Repositories;

public interface ICustomerRepository
{
	Task<Entities.Customer?> FindCustomerByIdAsync(Guid customerid);
	Task<Entities.Customer?> FindCustomerByEmailAsync(string email);
	Task UpdateCustomerAsync(Entities.Customer customer);
}