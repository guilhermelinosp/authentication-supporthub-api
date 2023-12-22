using SupportHub.Domain.DTOs.Requests;
using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Companies.SignUp;

public interface ISignUpUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestSignUp request);
}