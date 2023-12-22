namespace SupportHub.Domain.DTOs.Requests.Companies;

public class RequestSignIn
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}