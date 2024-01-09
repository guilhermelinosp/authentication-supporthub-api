using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.ForgotPassword;

public interface IForgotPasswordUseCase
{
	Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request);
}