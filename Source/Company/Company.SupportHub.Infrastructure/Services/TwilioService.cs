using Microsoft.Extensions.Configuration;
using Company.SupportHub.Domain.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Company.SupportHub.Infrastructure.Services;

public class TwilioService(IConfiguration configuration) : ITwilioService, IInfrastructureInjection
{
	public async Task SendConfirmationAsync(string phone, string code)
	{
		TwilioClient.Init(configuration["Twilio_AccountSID"], configuration["Twilio_AuthToken"]);

		await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phone))
		{
			From = configuration["Twilio_FromNumber"],
			Body = $"Your security code for Test API: {code}"
		});
	}

	public async Task SendSignInAsync(string phone, string code)
	{
		TwilioClient.Init(configuration["Twilio_AccountSID"], configuration["Twilio_AuthToken"]);

		await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber(phone))
		{
			From = configuration["Twilio_FromNumber"],
			Body = $"Your security code for Test API: {code}"
		});
	}
}