using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Customers.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}