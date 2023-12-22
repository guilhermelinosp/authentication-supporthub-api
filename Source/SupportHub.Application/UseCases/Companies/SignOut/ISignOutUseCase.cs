using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}