using Authentication.SupportHub.Domain.Entities;

namespace Authentication.SupportHub.Domain.Repositories;

public interface IAccountRepository
{
	Task<Account?> FindAccountByIdAsync(Guid accountId);
	Task<Account?> FindAccountByEmailAsync(string email);
	Task<Account?> FindAccountByIdentityAsync(string identity);
	Task CreateAccountAsync(Account account);
	Task UpdateAccountAsync(Account account);
}