namespace Employee.SupportHub.Domain.DTOs.Requests;

public class RequestResetPassword
{
	public required string Password { get; set; }
	public required string PasswordConfirmation { get; set; }
}