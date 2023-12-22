using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Companies.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}