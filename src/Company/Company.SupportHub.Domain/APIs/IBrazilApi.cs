namespace Company.SupportHub.Domain.APIs;

public interface IBrazilApi
{
	Task<bool> ConsultaCnpj(string cnpj);
}