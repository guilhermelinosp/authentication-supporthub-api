using SupportHub.Domain.DTOs.Responses;

namespace SupportHub.Application.UseCases.Employees.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}