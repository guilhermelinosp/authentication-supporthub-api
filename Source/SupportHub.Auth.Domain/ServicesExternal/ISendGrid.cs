namespace SupportHub.Auth.Domain.ServicesExternal;

public interface ISendGrid
{
    Task SendSignUpAsync(string email, string code);
    Task SendForgotPasswordAsync(string email, string code);
    Task SendSignInAsync(string email, string code);
}