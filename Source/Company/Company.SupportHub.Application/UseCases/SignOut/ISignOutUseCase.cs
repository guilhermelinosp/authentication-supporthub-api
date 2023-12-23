using Company.SupportHub.Domain.DTOs.Responses;

namespace Company.SupportHub.Application.UseCases.SignOut;

public interface ISignOutUseCase : IApplicationInjection
{
	Task<ResponseDefault> ExecuteAsync(string token);
}