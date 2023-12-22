using Newtonsoft.Json;
using SupportHub.Domain.APIs;
using SupportHub.Domain.DTOs.Responses.APIs;
using SupportHub.Domain.Exceptions;
using SupportHub.Domain.Shared.Returns;

namespace SupportHub.Auth.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task<BasicReturn<ResponseCnpj>> ConsultaCnpj(string cnpj)
	{
		var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"/cnpj/v1/{cnpj}");

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