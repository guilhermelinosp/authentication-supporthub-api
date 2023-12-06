namespace SupportHub.Auth.Domain.Dtos.Requests.Companies;

public class RequestSignInEmail
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}