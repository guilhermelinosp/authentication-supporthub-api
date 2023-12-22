using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Customers.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}