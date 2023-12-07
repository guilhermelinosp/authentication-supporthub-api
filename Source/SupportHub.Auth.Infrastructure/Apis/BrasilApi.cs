using Newtonsoft.Json;
using SupportHub.Auth.Domain.Apis;
using SupportHub.Auth.Domain.Dtos.Responses.Apis.Brasil;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.Infrastructure.Apis;

public class BrasilApi(HttpClient httpClient) : IBrasilApi
{
    public async Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"v1/{cnpj}");

        using var response = await httpClient.SendAsync(httpRequest);
        string cnpjReturn = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            ResponseCnpj? responseCnpj = JsonConvert.DeserializeObject<ResponseCnpj>(cnpjReturn);
            return responseCnpj;
        }

        var error = JsonConvert.DeserializeObject<ErrorResponse>(cnpjReturn);
        return BasicReturn.Failure<ResponseCnpj>(new(response.StatusCode, error!.Message));
    }
}