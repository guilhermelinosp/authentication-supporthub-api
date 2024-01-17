using Authentication.SupportHub.Domain.APIs;
using Authentication.SupportHub.Domain.DTOs.Messages;
using Authentication.SupportHub.Domain.DTOs.Responses.BrazilAPI;
using Authentication.SupportHub.Domain.Exceptions;
using Newtonsoft.Json;

namespace Authentication.SupportHub.Infrastructure.APIs;

public class BrazilApi(HttpClient httpClient) : IBrazilAPI
{
	public async Task<ResponseBrazilAPI?> Consultation(string cnpj)
	{
		try
		{
			using var request = await httpClient.SendAsync(
				new HttpRequestMessage(HttpMethod.Get, $"/api/cnpj/v1/{cnpj}"));

			var response = await request.Content.ReadAsStringAsync();


			if (!request.IsSuccessStatusCode)
			{
				throw new DefaultException([MessageException.IDENTITY_INVALIDO]);
			}

			return JsonConvert.DeserializeObject<ResponseBrazilAPI>(response);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			throw;
		}
	}
}