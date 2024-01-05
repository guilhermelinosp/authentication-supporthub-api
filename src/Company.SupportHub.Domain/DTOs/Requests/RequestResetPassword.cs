namespace Company.SupportHub.Domain.DTOs.Requests;

public class RequestResetPassword(string password, string passwordConfirmation)
{
	public required string Password { get; set; } = password;
	public required string PasswordConfirmation { get; set; } = passwordConfirmation;
}