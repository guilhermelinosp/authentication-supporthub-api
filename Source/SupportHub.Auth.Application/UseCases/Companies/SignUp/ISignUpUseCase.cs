using SupportHub.Auth.Application.Abstract;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp;

public interface ISignUpUseCase : IUseCaseBase
{
    Task ExecuteAsync(RequestSignUp request);
}