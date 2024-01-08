namespace Authentication.SupportHub.Domain.Services;

public interface ITwilioService
{
	Task SendSignInAsync(string phone, string code);
}