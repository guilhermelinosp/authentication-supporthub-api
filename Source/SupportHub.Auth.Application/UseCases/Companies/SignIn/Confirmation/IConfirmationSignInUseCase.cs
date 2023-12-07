using SupportHub.Auth.Application.Abstract;
using SupportHub.Auth.Domain.Dtos.Responses.Companies;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public interface IConfirmationSignInUseCase : IUseCaseBase
{
    Task<ResponseSignIn> ExecuteAsync(string code);
}