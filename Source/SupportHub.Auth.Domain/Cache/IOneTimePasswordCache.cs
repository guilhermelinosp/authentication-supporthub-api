namespace SupportHub.Auth.Domain.Cache;

public interface IOneTimePasswordCache
{
	string GenerateOneTimePassword(string accountId);
	
	bool ValidateOneTimePassword(string accountId, string otpCode);
}