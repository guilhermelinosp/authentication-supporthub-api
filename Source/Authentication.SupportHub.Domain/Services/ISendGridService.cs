namespace Authentication.SupportHub.Domain.Services;

public interface ISendGridService
{
	Task SendSignUpAsync(string email, string code);
	Task SendForgotPasswordAsync(string email, string code);
	Task SendSignInAsync(string email, string code);
}