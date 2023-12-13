using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp;

public interface ISignUpUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestSignUp request);
}