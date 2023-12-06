using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp;

public interface ISignUpUseCase
{
    Task ExecuteAsync(RequestSignUp request);
}