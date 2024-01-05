using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.Employee.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseToken> ExecuteAsync(RequestSignInEmployee request);
}