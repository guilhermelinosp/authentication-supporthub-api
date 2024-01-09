namespace Authentication.SupportHub.Application.Services.Tokenization;

public interface ITokenizationService
{
	string GenerateToken(string id);
	string GenerateRefreshToken();
	Guid ValidateToken(string token);
}