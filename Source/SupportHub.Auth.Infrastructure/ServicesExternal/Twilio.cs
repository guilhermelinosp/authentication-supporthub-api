using Microsoft.Extensions.Configuration;
using SupportHub.Auth.Domain.ServicesExternal;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace SupportHub.Auth.Infrastructure.ServicesExternal;

public class Twilio(IConfiguration configuration):ITwilio
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