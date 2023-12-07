using Newtonsoft.Json;
using SupportHub.Auth.Domain.Apis;
using SupportHub.Auth.Domain.Dtos.Responses.Apis.Brasil;

namespace SupportHub.Auth.Infrastructure.Apis;

public class BrasilApi(HttpClient httpClient) : IBrasilApi
{
    public async Task ConsultaCnpj(string cnpj)
    {
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"v1/{cnpj}");

        using var response = await httpClient.SendAsync(httpRequest);
        string cnpjReturn = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            ResponseCNPJ? responseCnpj = JsonConvert.DeserializeObject<ResponseCNPJ>(cnpjReturn);
        }
    }
}