using Authentication.SupportHub.Domain.DTOs.Responses;

namespace Authentication.SupportHub.Application.UseCases.Account.SignOut;

public interface ISignOutUseCase
{
	Task<ResponseDefault> ExecuteAsync(string token);
}