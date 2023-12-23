using Employee.SupportHub.Domain.DTOs.Responses;

namespace Employee.SupportHub.Application.UseCases.Employees.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}