using SupportHub.Auth.Domain.DTOs.Responses;

namespace SupportHub.Auth.Application.UseCases.Companies.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}