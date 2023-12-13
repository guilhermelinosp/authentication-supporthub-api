using Newtonsoft.Json;
using StackExchange.Redis;
using SupportHub.Auth.Domain.Cache;

namespace SupportHub.Auth.Infrastructure.Cache
{
	public class SessionCache(IConnectionMultiplexer redisConnection) : ISessionCache
	{
		public void SetSessionAccountAsync(string accountId)
		{
			var redisDb = redisConnection.GetDatabase();

			var session = new Session { SessionIsActive = true };
			var sessionJson = JsonConvert.SerializeObject(session);

			if (!string.IsNullOrEmpty(sessionJson))
			{
				redisDb.StringSet(accountId, sessionJson, TimeSpan.FromMinutes(720));
			}
		}
		
		public void OutSessionAccountAsync(string accountId)
		{
			var redisDb = redisConnection.GetDatabase();
			redisDb.KeyDelete(accountId);
		}

		public bool ValidateSessionAsync(string accountId)
		{
			var redisDb = redisConnection.GetDatabase();
			var sessionJson = redisDb.StringGet(accountId);

			if (!string.IsNullOrEmpty(sessionJson))
			{
				var storedSession = JsonConvert.DeserializeObject<Session>(sessionJson!);
				return storedSession?.SessionIsActive ?? false;
			}

			return false;
		}
	}

	public class Session
	{
		public bool SessionIsActive { get; set; }
	}
}