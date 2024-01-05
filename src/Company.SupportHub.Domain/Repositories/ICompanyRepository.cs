using Company.SupportHub.Domain.Entities;

namespace Company.SupportHub.Domain.Repositories;

public interface ICompanyRepository
{
	Task<AccountCompany?> FindCompanyByIdAsync(Guid companyid);
	Task<AccountCompany?> FindCompanyByEmailAsync(string email);
	Task<AccountCompany?> FindCompanyByCnpjAsync(string cnpj);
	Task CreateCompanyAsync(AccountCompany accountCompany);
	Task UpdateCompanyAsync(AccountCompany accountCompany);
}