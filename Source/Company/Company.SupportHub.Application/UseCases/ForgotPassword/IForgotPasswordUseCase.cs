using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}