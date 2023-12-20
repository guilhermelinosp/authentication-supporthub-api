using SupportHub.Authentication.Domain.DTOs.Requests.Companies;
using SupportHub.Authentication.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
    Task<ResponseDefault> ExecuteAsync(RequestResetPassword request,string accountId, string code);
}