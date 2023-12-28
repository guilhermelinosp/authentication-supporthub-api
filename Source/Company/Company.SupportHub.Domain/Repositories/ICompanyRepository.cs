using Company.SupportHub.Domain.Entities;
using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Repositories;

public interface ICompanyRepository:IInfrastructureInjection
{
	Task<Entities.Company?> FindCompanyByIdAsync(Guid companyid);
	Task<Entities.Company?> FindCompanyByEmailAsync(string email);
	Task<Entities.Company?> FindCompanyByCnpjAsync(string cnpj);
	Task CreateCompanyAsync(Entities.Company company);
	Task UpdateCompanyAsync(Entities.Company company);
}