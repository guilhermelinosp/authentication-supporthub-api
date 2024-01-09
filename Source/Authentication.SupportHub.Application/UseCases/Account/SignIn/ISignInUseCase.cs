using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignIn;

public interface ISignInUseCase
{
	Task<ResponseDefault> ExecuteAsync(RequestSignInAccount request);
}