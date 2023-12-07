namespace SupportHub.Auth.Domain.Apis;

public interface IBrasilApi
{
    Task ConsultaCnpj(string cnpj);
}