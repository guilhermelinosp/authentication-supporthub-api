using Company.SupportHub.Domain.Services;
using Company.SupportHub.Domain.VOs;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Company.SupportHub.Infrastructure.Services;

public class RedisService(IConnectionMultiplexer redisConnection) : IRedisService, IInfrastructureInjection
{
	public string GenerateOneTimePassword(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var otp = new Random().Next(100000, 999999).ToString();

		var otpObject = new OneTimePassword { Code = otp };
		var otpJson = JsonConvert.SerializeObject(otpObject);

		redisDb.StringSet($"OTP-{accountId}", otpJson, TimeSpan.FromMinutes(5));

		return otp;
	}

	public bool ValidateOneTimePassword(string accountId, string otpCode)
	{
		var redisDb = redisConnection.GetDatabase();
		var storedOtpJson = redisDb.StringGet($"OTP-{accountId}");

		if (string.IsNullOrEmpty(storedOtpJson)) return false;

		var storedOtp = JsonConvert.DeserializeObject<OneTimePassword>(storedOtpJson!);

		return storedOtp?.Code == otpCode;
	}

	public void SetSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = JsonConvert.SerializeObject(new SessionStorage { SessionIsActive = true });

		redisDb.StringSet($"S-{accountId}", session, TimeSpan.FromMinutes(720));
	}

	public void OutSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var session = JsonConvert.SerializeObject(new SessionStorage { SessionIsActive = false });

		redisDb.StringSet($"S-{accountId}", session, TimeSpan.FromMinutes(720));
	}

	public bool ValidateSessionAccountAsync(string accountId)
	{
		var redisDb = redisConnection.GetDatabase();

		var sessionJson = redisDb.StringGet($"S-{accountId}");

		if (string.IsNullOrEmpty(sessionJson)) return false;

		var storedSession = JsonConvert.DeserializeObject<SessionStorage>(sessionJson!);

		return storedSession!.SessionIsActive;
	}
}