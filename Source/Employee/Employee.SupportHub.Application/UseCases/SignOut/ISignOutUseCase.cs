using Employee.SupportHub.Domain.DTOs.Responses;

namespace Employee.SupportHub.Application.UseCases.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}