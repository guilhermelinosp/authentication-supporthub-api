using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestSignIn request);
}