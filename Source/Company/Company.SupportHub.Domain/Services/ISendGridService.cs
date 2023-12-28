using Customer.SupportHub.Infrastructure;

namespace Company.SupportHub.Domain.Services;

public interface ISendGridService : IInfrastructureInjection
{
	Task SendSignUpAsync(string email, string code);
	Task SendForgotPasswordAsync(string email, string code);
	Task SendSignInAsync(string email, string code);
}