using SupportHub.Authentication.Domain.DTOs.Responses.APIs;
using SupportHub.Authentication.Domain.Shared.Returns;

namespace SupportHub.Authentication.Domain.APIs;

public interface IBrazilApi
{
	Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj);
}