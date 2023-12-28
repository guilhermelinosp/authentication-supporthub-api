using Customer.SupportHub.Domain.Services;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Customer.SupportHub.Infrastructure.Services;

public class RedisService(IConnectionMultiplexer redisConnection) : IRedisService
{
	public string GenerateOneTimePassword(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var otp = new Random().Next(100000, 999999).ToString();

		var otpObject = new Otp { Code = otp };
		var otpJson = JsonConvert.SerializeObject(otpObject);

		redisDb.StringSet($"OTP-{accountId}", otpJson, TimeSpan.FromMinutes(5));

		return otp;
	}

	public bool ValidateOneTimePassword(string accountId, string otpCode)
	{
		var redisDb = redisConnection.GetDatabase();
		var storedOtpJson = redisDb.StringGet($"OTP-{accountId}");

		if (string.IsNullOrEmpty(storedOtpJson)) return false;

		var storedOtp = JsonConvert.DeserializeObject<Otp>(storedOtpJson!);

		return storedOtp?.Code == otpCode;
	}

	public void SetSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = JsonConvert.SerializeObject(new SessionAccount { SessionIsActive = true });

		redisDb.StringSet($"S-{accountId}", session, TimeSpan.FromMinutes(720));
	}

	public void OutSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = JsonConvert.SerializeObject(new SessionAccount { SessionIsActive = false });

		redisDb.StringSet($"S-{accountId}", session, TimeSpan.FromMinutes(720));
	}

	public bool ValidateSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var sessionJson = redisDb.StringGet($"S-{accountId}");

		if (string.IsNullOrEmpty(sessionJson)) return false;

		var storedSession = JsonConvert.DeserializeObject<SessionAccount>(sessionJson!);

		return storedSession!.SessionIsActive!;
	}
}