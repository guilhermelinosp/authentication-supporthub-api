using Azure;
using Customer.SupportHub.Domain.APIs;
using Customer.SupportHub.Domain.Exceptions;
using Customer.SupportHub.Domain.Messages;
using Newtonsoft.Json;

namespace Customer.SupportHub.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task ConsultaCnpj(string cnpj)
	{
		using var request = await httpClient.SendAsync(
			new HttpRequestMessage(HttpMethod.Get, $"/api/cnpj/v1/{cnpj}"));

		if (!request.IsSuccessStatusCode) throw new DefaultException([MessageException.CNPJ_INVALIDO]);
	}
}