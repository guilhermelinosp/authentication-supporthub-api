using Microsoft.EntityFrameworkCore;
using Company.SupportHub.Domain.Entities;
using Company.SupportHub.Domain.Repositories;
using Company.SupportHub.Infrastructure.Contexts;

namespace Company.SupportHub.Infrastructure.Repositories;

public class CompanyRepository(CompanyDbContext context) : ICompanyRepository, IInfrastructureInjection
{
	public async Task<Domain.Entities.Company?> FindCompanyByIdAsync(Guid companyid)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.CompanyId == companyid);
	}

	public async Task<Domain.Entities.Company?> FindCompanyByEmailAsync(string email)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Email == email);
	}

	public async Task<Domain.Entities.Company?> FindCompanyByPhoneAsync(string phone)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Phone == phone);
	}

	public async Task<Domain.Entities.Company?> FindCompanyByCnpjAsync(string cnpj)
	{
		return await context.Companies!.AsNoTracking().SingleOrDefaultAsync(u => u.Cnpj == cnpj);
	}

	public async Task CreateCompanyAsync(Domain.Entities.Company company)
	{
		await context.Companies!.AddAsync(company);

		await SaveChangesAsync();
	}

	public async Task UpdateCompanyAsync(Domain.Entities.Company company)
	{
		context.Companies!.Update(company);

		await SaveChangesAsync();
	}

	public async Task DeleteCompanyAsync(Domain.Entities.Company company)
	{
		context.Companies!.Remove(company);

		await SaveChangesAsync();
	}

	private async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}
}