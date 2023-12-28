using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.APIs;

public interface IBrazilApi : IInfrastructureInjection
{
	Task ConsultaCnpj(string cnpj);
}