using SupportHub.Domain.DTOs.Requests.Companies;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Companies.ForgotPassword;

public interface IForgotPasswordUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}