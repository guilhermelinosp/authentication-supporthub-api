using Microsoft.Extensions.Configuration;
using SupportHub.Authentication.Domain.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SupportHub.Auth.Infrastructure.Services;

public class TwilioServiceService(IConfiguration configuration) : ITwilioService
{
	public async Task SendConfirmationAsync(string phone, string code)
	{
		TwilioClient.Init(configuration["Twilio:AccountSID"], configuration["Twilio:AuthToken"]);

		await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phone))
		{
			From = configuration["Twilio:FromNumber"],
			Body = $"Your security code for Test API: {code}"
		});
	}

	public async Task SendSignInAsync(string phone, string code)
	{
		TwilioClient.Init(configuration["Twilio:AccountSID"], configuration["Twilio:AuthToken"]);

		await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phone))
		{
			From = configuration["Twilio:FromNumber"],
			Body = $"Your security code for Test API: {code}"
		});
	}
}