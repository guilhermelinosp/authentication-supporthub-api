using Newtonsoft.Json;
using StackExchange.Redis;
using Company.SupportHub.Domain.Cache;

namespace Company.SupportHub.Infrastructure.Cache;

public class SessionCache(IConnectionMultiplexer redisConnection) : ISessionCache
{
	public void SetSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = new Session { SessionIsActive = true };
		var sessionJson = JsonConvert.SerializeObject(session);

		if (!string.IsNullOrEmpty(sessionJson))
			redisDb.StringSet($"S-{accountId}", sessionJson, TimeSpan.FromMinutes(720));
	}

	public void OutSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = new Session { SessionIsActive = false };
		var sessionJson = JsonConvert.SerializeObject(session);

		if (!string.IsNullOrEmpty(sessionJson))
			redisDb.StringSet($"S-{accountId}", sessionJson, TimeSpan.FromMinutes(720));
	}

	public bool ValidateSessionAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();
		var sessionJson = redisDb.StringGet($"S-{accountId}");

		if (!string.IsNullOrEmpty(sessionJson))
		{
			var storedSession = JsonConvert.DeserializeObject<Session>(sessionJson!);
			return storedSession?.SessionIsActive == true;
		}

		return false;
	}
}