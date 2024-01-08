namespace Authentication.SupportHub.Domain.DTOs.Responses;

public class ResponseToken
{
	public required string Token { get; set; }
	public required string RefreshToken { get; set; }
	public DateTime ExpiryDate { get; set; }
}