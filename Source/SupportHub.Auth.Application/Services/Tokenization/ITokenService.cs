namespace SupportHub.Auth.Application.Services.Tokenization;

public interface ITokenService
{
    string GenerateToken(string id);
    string GenerateRefreshToken();
    Guid ValidateToken(string token);
}