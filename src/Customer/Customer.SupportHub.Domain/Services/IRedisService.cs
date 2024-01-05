namespace Customer.SupportHub.Domain.Services;

public interface IRedisService
{
	string GenerateOneTimePassword(string accountId);

	bool ValidateOneTimePassword(string accountId, string otpCode);

	void SetSessionStorage(string accountId);

	void OutSessionStorage(string accountId);

	bool ValidateSessionStorage(string accountId);
}