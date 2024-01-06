using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.Company.SignIn;

public interface ISignInUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(RequestSignInCompany request);
}