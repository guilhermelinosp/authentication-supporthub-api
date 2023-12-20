using SupportHub.Authentication.Domain.DTOs.Requests.Companies;
using SupportHub.Authentication.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}