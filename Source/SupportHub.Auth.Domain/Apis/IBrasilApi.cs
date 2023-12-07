using SupportHub.Auth.Domain.Dtos.Responses.Apis.Brasil;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.Domain.Apis;

public interface IBrasilApi
{
    Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj);
}