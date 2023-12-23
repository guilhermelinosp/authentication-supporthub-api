namespace Company.SupportHub.Infrastructure.Services;

public interface IRedisService
{
	string GenerateOneTimePassword(string accountId);

	bool ValidateOneTimePassword(string accountId, string otpCode);
	
	void SetSessionAccountAsync(string accountId);
	
	void OutSessionAccountAsync(string accountId);
	
	bool ValidateSessionAccountAsync(string accountId);
}