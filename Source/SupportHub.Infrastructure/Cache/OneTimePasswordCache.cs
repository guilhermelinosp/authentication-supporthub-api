using Newtonsoft.Json;
using StackExchange.Redis;
using SupportHub.Domain.Cache;

namespace SupportHub.Infrastructure.Cache;

public class OneTimePasswordCache(IConnectionMultiplexer redisConnection) : IOneTimePasswordCache
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

		if (!string.IsNullOrEmpty(storedOtpJson))
		{
			var storedOtp = JsonConvert.DeserializeObject<Otp>(storedOtpJson!);
			return storedOtp?.Code == otpCode;
		}

		return false;
	}
}

public class Otp
{
	public string? Code { get; set; }
}