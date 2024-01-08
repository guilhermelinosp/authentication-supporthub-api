using Authentication.SupportHub.Domain.APIs;

namespace Authentication.SupportHub.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task<bool> ConsultaCnpj(string cnpj)
	{
		using var request = await httpClient.SendAsync(
			new HttpRequestMessage(HttpMethod.Get, $"/api/cnpj/v1/{cnpj}"));

		return request.IsSuccessStatusCode;
	}
}