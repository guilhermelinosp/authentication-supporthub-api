namespace SupportHub.Auth.Domain.Dtos.Requests.Companies;

public class RequestResetPassword
{
    public required string Password { get; set; }
    public required string PasswordConfirmation { get; set; }
}