using Newtonsoft.Json;
using SupportHub.Authentication.Domain.APIs;
using SupportHub.Authentication.Domain.DTOs.Responses.APIs;
using SupportHub.Authentication.Domain.Exceptions;
using SupportHub.Authentication.Domain.Shared.Returns;

namespace SupportHub.Auth.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj)
	{
		var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"v1/{cnpj}");

		using var response = await httpClient.SendAsync(httpRequest);
		var cnpjReturn = await response.Content.ReadAsStringAsync();

		if (response.IsSuccessStatusCode)
		{
			var responseCnpj = JsonConvert.DeserializeObject<ResponseCnpj>(cnpjReturn);
			return responseCnpj;
		}


		var error = JsonConvert.DeserializeObject<ErrorResponse>(cnpjReturn);

		//throw new DefaultException([error!.Message]);
		return BasicReturn.Failure<ResponseCnpj>(new Error(response.StatusCode, error!.Message));
	}
}