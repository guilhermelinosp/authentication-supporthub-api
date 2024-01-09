namespace Authentication.SupportHub.Domain.DTOs.Requests;

public class RequestSignUpAccount
{
	public required string Cnpj { get; set; }
	public required string Email { get; set; }
	public required string Password { get; set; }
	public required string PasswordConfirmation { get; set; }
}