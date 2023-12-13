using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestResetPassword request,string accountId, string code);
}