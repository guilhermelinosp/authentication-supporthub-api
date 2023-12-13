using SupportHub.Auth.Domain.DTOs.Responses.APIs;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.Domain.APIs;

public interface IBrazilApi
{
    Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj);
}