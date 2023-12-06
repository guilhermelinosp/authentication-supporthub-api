using SupportHub.Auth.Domain.Dtos.Responses.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public interface IConfirmationSignInUseCase
{
    Task<ResponseSignIn> ExecuteAsync(string code);
}