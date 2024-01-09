using Microsoft.Extensions.Configuration;
using Authentication.SupportHub.Domain.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Authentication.SupportHub.Infrastructure.Services;

public class TwilioService(IConfiguration configuration) : ITwilioService
{
	public async Task SendSignInAsync(string phone, string code)
	{
		TwilioClient.Init(configuration["Twilio:AccountSID"], configuration["Twilio:AuthToken"]);

		await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phone))
		{
			From = configuration["Twilio_FromNumber"],
			Body = $"Your security code - Support Hub: {code}"
		});
	}
}