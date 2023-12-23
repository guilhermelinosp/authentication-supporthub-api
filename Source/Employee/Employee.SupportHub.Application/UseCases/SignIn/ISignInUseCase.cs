using Employee.SupportHub.Domain.DTOs.Requests;
using Employee.SupportHub.Domain.DTOs.Responses;

namespace Employee.SupportHub.Application.UseCases.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseToken> ExecuteAsync(RequestSignIn request);
}