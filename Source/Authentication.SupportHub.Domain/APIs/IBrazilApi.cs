namespace Authentication.SupportHub.Domain.APIs;

public interface IBrazilApi
{
	Task<bool> Consultation(string cnpj);
}