using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestSignIn request);
}