using Microsoft.EntityFrameworkCore;
using Authentication.SupportHub.Domain.Entities;
using Authentication.SupportHub.Domain.Repositories;
using Authentication.SupportHub.Infrastructure.Contexts;

namespace Authentication.SupportHub.Infrastructure.Repositories;

public class AccountRepository(AuthenticationDbContext context) : IAccountRepository
{
	public async Task<Account?> FindAccountByIdAsync(Guid accountId)
	{
		return await context.Accounts!.AsNoTracking().SingleOrDefaultAsync(u => u.AccountId == accountId);
	}

	public async Task<Account?> FindAccountByEmailAsync(string email)
	{
		return await context.Accounts!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task<Account?> FindCompanyByPhoneAsync(string phone)
	{
		return await context.Accounts!.AsNoTracking().SingleOrDefaultAsync(u => u.Phone == phone);
	}

	public async Task<Account?> FindAccountByIdentityAsync(string identity)
	{
		return await context.Accounts!.AsNoTracking().SingleOrDefaultAsync(u => u.Identity == identity);
	}

	public async Task CreateAccountAsync(Account account)
	{
		await context.Accounts!.AddAsync(account);

		await SaveChangesAsync();
	}

	public async Task UpdateAccountAsync(Account account)
	{
		context.Accounts!.Update(account);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}