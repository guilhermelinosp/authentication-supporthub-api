using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;

namespace Customer.SupportHub.Application.UseCases.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}