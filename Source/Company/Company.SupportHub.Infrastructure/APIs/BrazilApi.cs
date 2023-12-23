using Newtonsoft.Json;
using Company.SupportHub.Domain.APIs;
using Company.SupportHub.Domain.DTOs.Responses.APIs;
using Company.SupportHub.Domain.Exceptions;

namespace Company.SupportHub.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task ConsultaCnpj(string cnpj)
	{
		var request = new HttpRequestMessage(HttpMethod.Get, $"/cnpj/v1/{cnpj}");

		using var response = await httpClient.SendAsync(request);
		var cnpjReturn = await response.Content.ReadAsStringAsync();

		if (!response.IsSuccessStatusCode)
			throw new DefaultException([JsonConvert.DeserializeObject<ErrorResponse>(cnpjReturn)?.Message!]);
	}
}