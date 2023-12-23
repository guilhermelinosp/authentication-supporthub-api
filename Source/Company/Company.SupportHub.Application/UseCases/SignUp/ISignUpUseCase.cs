using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.SignUp;

public interface ISignUpUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestSignUp request);
}