namespace SupportHub.Auth.Application.Services.Tokenization;

public interface ITokenizationService : IApplicationInjection
{
    string GenerateToken(string id);
    string GenerateRefreshToken();
    Guid ValidateToken(string token);
}