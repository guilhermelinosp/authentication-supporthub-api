namespace SupportHub.Auth.Domain.Dtos.Requests.Companies;

public class RequestSignUp
{
    public required string Cnpj { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirmation { get; set; }
}