using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Employee.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}