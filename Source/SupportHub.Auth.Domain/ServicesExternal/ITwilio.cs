namespace SupportHub.Auth.Domain.ServicesExternal;

public interface ITwilio
{
    Task SendConfirmationAsync(string phone, string code);
    Task SendSignInAsync(string phone, string code);
}