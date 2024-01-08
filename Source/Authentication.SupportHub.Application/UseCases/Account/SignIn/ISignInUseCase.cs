using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestSignInCompany request);
}