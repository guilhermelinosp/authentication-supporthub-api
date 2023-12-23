using Customer.SupportHub.Domain.APIs;
using Customer.SupportHub.Domain.DTOs.Responses.APIs;
using Customer.SupportHub.Domain.Exceptions;
using Newtonsoft.Json;

namespace Customer.SupportHub.Infrastructure.APIs;

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