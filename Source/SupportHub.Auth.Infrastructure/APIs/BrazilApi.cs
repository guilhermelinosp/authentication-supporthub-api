using Newtonsoft.Json;
using SupportHub.Auth.Domain.APIs;
using SupportHub.Auth.Domain.DTOs.Responses.APIs;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Shared.Returns;

namespace SupportHub.Auth.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
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

        //throw new DefaultException([error!.Message]);
        return BasicReturn.Failure<ResponseCnpj>(new(response.StatusCode, error!.Message));
    }
}