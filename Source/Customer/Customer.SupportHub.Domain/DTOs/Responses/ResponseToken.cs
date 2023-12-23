namespace Customer.SupportHub.Domain.DTOs.Responses;

public class ResponseToken
{
	public string Token { get; set; }
	public string RefreshToken { get; set; }
	public DateTime ExpiryDate { get; set; }
}