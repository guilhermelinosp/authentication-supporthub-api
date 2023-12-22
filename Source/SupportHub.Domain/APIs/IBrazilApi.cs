using SupportHub.Domain.DTOs.Responses.APIs;
using SupportHub.Domain.Shared.Returns;

namespace SupportHub.Domain.APIs;

public interface IBrazilApi
{
	Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj);
}