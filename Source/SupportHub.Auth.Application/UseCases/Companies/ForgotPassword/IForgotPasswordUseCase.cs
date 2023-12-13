using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}