using Microsoft.EntityFrameworkCore;
using SupportHub.Auth.Domain.Entities;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Infrastructure.Contexts;

namespace SupportHub.Auth.Infrastructure.Repositories;

public class CompanyRepository(SupportHubContext context) : ICompanyRepository
{
    public async Task<Company?> FindCompanyByIdAsync(Guid companyid)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(u => u.CompanyId == companyid);
    }

    public async Task<Company?> FindCompanyByEmailAsync(string email)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Company?> FindCompanyByPhoneAsync(string phone)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(u => u.Phone == phone);
    }

    public async Task<Company?> FindCompanyByCnpjAsync(string cnpj)
    {
        return await context.Companies.AsNoTracking().FirstOrDefaultAsync(u => u.Cnpj == cnpj);
    }

    public async Task<Company?> FindCompanyByCodeAsync(string code)
    {
        return await context.Companies.FirstOrDefaultAsync(u => u.Code == code);
    }

    public async Task CreateCompanyAsync(Company company)
    {
        await context.Companies.AddAsync(company);

        await SaveChangesAsync();
    }

    public async Task UpdateCompanyAsync(Company company)
    {
        context.Companies.Update(company);

        await SaveChangesAsync();
    }

    public async Task DeleteCompanyAsync(Company company)
    {
        context.Companies.Remove(company);

        await SaveChangesAsync();
    }

    private async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}