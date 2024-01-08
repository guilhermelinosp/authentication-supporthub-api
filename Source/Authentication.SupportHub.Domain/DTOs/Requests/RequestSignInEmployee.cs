namespace Authentication.SupportHub.Domain.DTOs.Requests;

public class RequestSignInEmployee
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}