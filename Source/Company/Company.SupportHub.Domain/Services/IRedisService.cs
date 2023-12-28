using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Services;

public interface IRedisService:IInfrastructureInjection
{
	string GenerateOneTimePassword(string accountId);

	bool ValidateOneTimePassword(string accountId, string otpCode);
	
	void SetSessionAccountAsync(string accountId);
	
	void OutSessionAccountAsync(string accountId);
	
	bool ValidateSessionAccountAsync(string accountId);
}