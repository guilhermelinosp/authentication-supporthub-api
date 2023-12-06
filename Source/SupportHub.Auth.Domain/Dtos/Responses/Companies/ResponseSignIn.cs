namespace SupportHub.Auth.Domain.Dtos.Responses.Companies;

public class ResponseSignIn
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
}