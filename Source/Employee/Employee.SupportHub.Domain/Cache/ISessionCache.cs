namespace Employee.SupportHub.Domain.Cache;

public interface ISessionCache
{
	void SetSessionAccountAsync(string accountId);
	void OutSessionAccountAsync(string accountId);
	bool ValidateSessionAsync(string accountId);
}