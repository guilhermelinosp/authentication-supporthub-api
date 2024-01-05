using Customer.SupportHub.Domain.Services;
using Customer.SupportHub.Domain.VOs;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Customer.SupportHub.Infrastructure.Services;

public class RedisService(IConnectionMultiplexer redisConnection) : IRedisService, IInfrastructureInjection
{
	public string GenerateOneTimePassword(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var otp = new Random().Next(100000, 999999).ToString();

		var jsonSerialize = JsonConvert.SerializeObject(new OneTimePassword { Code = otp });

		redisDb.StringSet($"OTP-{accountId}", jsonSerialize, TimeSpan.FromMinutes(5));

		return otp;
	}

	public bool ValidateOneTimePassword(string accountId, string otpCode)
	{
		var redisDb = redisConnection.GetDatabase();

		var json = redisDb.StringGet($"OTP-{accountId}");

		if (string.IsNullOrEmpty(json)) return false;

		var jsonSerialize = JsonConvert.DeserializeObject<OneTimePassword>(json!);

		return jsonSerialize?.Code == otpCode;
	}

	public void SetSessionStorage(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var jsonSerialize = JsonConvert.SerializeObject(new SessionStorage { SessionIsActive = true });

		redisDb.StringSet($"S-{accountId}", jsonSerialize, TimeSpan.FromMinutes(720));
	}

	public void OutSessionStorage(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var jsonSerialize = JsonConvert.SerializeObject(new SessionStorage { SessionIsActive = false });

		redisDb.StringSet($"S-{accountId}", jsonSerialize, TimeSpan.FromMinutes(720));
	}

	public bool ValidateSessionStorage(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var json = redisDb.StringGet($"S-{accountId}");

		if (string.IsNullOrEmpty(json)) return false;

		var jsonSerialize = JsonConvert.DeserializeObject<SessionStorage>(json!);

		return jsonSerialize!.SessionIsActive;
	}
}