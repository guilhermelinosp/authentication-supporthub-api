using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Employees.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseToken> ExecuteAsync(RequestSignIn request);
}