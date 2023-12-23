using Customer.SupportHub.Domain.DTOs.Responses;

namespace Customer.SupportHub.Application.UseCases.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}