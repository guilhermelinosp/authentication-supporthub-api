using Authentication.SupportHub.Domain.Entities;

namespace Authentication.SupportHub.Domain.Repositories;

public interface IAccountRepository
{
	Task<Account?> FindAccountByIdAsync(Guid accountId);
	Task<Account?> FindAccountByEmailAsync(string email);
	Task<Account?> FindAccountByCnpjAsync(string cnpj);
	Task CreateAccountAsync(Account account);
	Task UpdateAccountAsync(Account account);
	Task DeleteAccountAsync(Account account);
}