using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Services;

public interface ITwilioService:IInfrastructureInjection
{
	Task SendConfirmationAsync(string phone, string code);
	Task SendSignInAsync(string phone, string code);
}