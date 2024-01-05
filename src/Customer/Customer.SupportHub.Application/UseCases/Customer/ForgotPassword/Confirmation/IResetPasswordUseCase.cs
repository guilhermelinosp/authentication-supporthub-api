using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;

namespace Customer.SupportHub.Application.UseCases.Customer.ForgotPassword.Confirmation;

public interface IResetPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestResetPassword request, string accountId, string code);
}