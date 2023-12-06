namespace SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;

public interface IConfirmationSignUpUseCase
{
    Task ExecuteAsync(string code);
}