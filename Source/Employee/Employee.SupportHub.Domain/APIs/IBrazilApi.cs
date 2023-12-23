namespace Employee.SupportHub.Domain.APIs;

public interface IBrazilApi
{
	Task ConsultaCnpj(string cnpj);
}