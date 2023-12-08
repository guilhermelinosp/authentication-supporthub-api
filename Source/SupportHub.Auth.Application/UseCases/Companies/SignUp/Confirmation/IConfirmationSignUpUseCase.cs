using SupportHub.Auth.Application.Abstract;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase : IUseCaseBase
{
    Task ExecuteAsync(string code);
}