using Newtonsoft.Json;
using Company.SupportHub.Domain.APIs;
using Company.SupportHub.Domain.DTOs.Responses.APIs;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;

namespace Company.SupportHub.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilApi
{
	public async Task ConsultaCnpj(string cnpj)
	{
		using var request = await httpClient.SendAsync(
			new HttpRequestMessage(HttpMethod.Get, $"/api/cnpj/v1/{cnpj}"));

		if (!request.IsSuccessStatusCode)
		{
			throw new ExceptionDefault([MessageException.CNPJ_INVALIDO]);
		}
	}
}