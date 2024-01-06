using Microsoft.EntityFrameworkCore;
using Company.SupportHub.Domain.Entities;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Infrastructure.Contexts;

namespace Company.SupportHub.Infrastructure.Repositories;

public class CompanyRepository(CompanyDbContext context) : ICompanyRepository, IInfrastructureInjection
{
	public async Task<AccountCompany?> FindCompanyByIdAsync(Guid companyid)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.CompanyId == companyid);
	}

	public async Task<AccountCompany?> FindCompanyByEmailAsync(string email)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task<AccountCompany?> FindCompanyByPhoneAsync(string phone)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Phone == phone);
	}

	public async Task<AccountCompany?> FindCompanyByCnpjAsync(string cnpj)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Cnpj == cnpj);
	}

	public async Task CreateCompanyAsync(AccountCompany accountCompany)
	{
		await context.Companies!.AddAsync(accountCompany);

		await SaveChangesAsync();
	}

	public async Task UpdateCompanyAsync(AccountCompany accountCompany)
	{
		context.Companies!.Update(accountCompany);

		await SaveChangesAsync();
	}

	public async Task DeleteCompanyAsync(AccountCompany accountCompany)
	{
		context.Companies!.Remove(accountCompany);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}