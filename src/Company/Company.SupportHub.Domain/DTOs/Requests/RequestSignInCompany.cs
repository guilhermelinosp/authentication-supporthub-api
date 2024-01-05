namespace Company.SupportHub.Domain.DTOs.Requests;

public class RequestSignInCompany
{
	public required string Cnpj { get; set; }
	public required string Password { get; set; }
}