namespace Customer.SupportHub.Domain.DTOs.Responses;

public class ResponseDefault(string accountId, string message)
{
	public string AccountId { get; set; } = accountId;
	public string Message { get; set; } = message;
}