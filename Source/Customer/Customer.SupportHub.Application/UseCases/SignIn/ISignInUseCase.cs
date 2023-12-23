using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.DTOs.Responses;

namespace Customer.SupportHub.Application.UseCases.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseToken> ExecuteAsync(RequestSignIn request);
}