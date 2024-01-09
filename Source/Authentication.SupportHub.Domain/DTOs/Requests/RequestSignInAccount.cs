namespace Authentication.SupportHub.Domain.DTOs.Requests;

public class RequestSignInAccount
{
	public required string Identity { get; set; }
	public required string Password { get; set; }
}