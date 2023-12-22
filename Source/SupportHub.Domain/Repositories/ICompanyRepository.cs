using SupportHub.Domain.Entities;

namespace SupportHub.Domain.Repositories;

public interface ICompanyRepository
{
	Task<Company?> FindCompanyByIdAsync(Guid companyid);
	Task<Company?> FindCompanyByEmailAsync(string email);
	Task<Company?> FindCompanyByPhoneAsync(string phone);
	Task<Company?> FindCompanyByCnpjAsync(string cnpj);
	Task CreateCompanyAsync(Company company);
	Task UpdateCompanyAsync(Company company);
	Task DeleteCompanyAsync(Company company);
}