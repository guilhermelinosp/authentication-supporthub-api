namespace Employee.SupportHub.Domain.DTOs.Requests;

public class RequestSignIn
{
	public required string Email { get; set; }
	public required string Password { get; set; }
}