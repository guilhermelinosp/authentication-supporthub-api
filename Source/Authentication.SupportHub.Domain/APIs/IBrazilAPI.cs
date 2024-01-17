using Authentication.SupportHub.Domain.DTOs.Responses.BrazilAPI;

namespace Authentication.SupportHub.Domain.APIs;

public interface IBrazilAPI
{
	Task<ResponseBrazilAPI?> Consultation(string cnpj);
}