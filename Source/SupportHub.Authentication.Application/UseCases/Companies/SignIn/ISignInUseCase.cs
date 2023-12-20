using SupportHub.Authentication.Domain.DTOs.Requests.Companies;
using SupportHub.Authentication.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestSignIn request);
}