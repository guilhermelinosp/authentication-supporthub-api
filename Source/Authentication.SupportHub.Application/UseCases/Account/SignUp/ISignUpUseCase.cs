using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignUp;

public interface ISignUpUseCase
{
	Task<ResponseDefault> ExecuteAsync(RequestSignUpAccount request);
}