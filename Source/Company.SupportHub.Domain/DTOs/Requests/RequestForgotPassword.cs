namespace Company.SupportHub.Domain.DTOs.Requests;

public class RequestForgotPassword(string email)
{
	public required string Email { get; set; } = email;
}