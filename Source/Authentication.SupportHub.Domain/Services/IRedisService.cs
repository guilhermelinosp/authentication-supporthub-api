namespace Authentication.SupportHub.Domain.Services;

public interface IRedisService
{
	string GenerateOneTimePassword(string accountId);

	bool ValidateOneTimePassword(string accountId, string otpCode);

	void SetSessionStorageAsync(string accountId);

	void OutSessionStorageAsync(string accountId);

	bool ValidateSessionStorageAsync(string accountId);
}