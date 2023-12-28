using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Services;

public interface ITwilioService
{
	Task SendConfirmationAsync(string phone, string code);
	Task SendSignInAsync(string phone, string code);
}