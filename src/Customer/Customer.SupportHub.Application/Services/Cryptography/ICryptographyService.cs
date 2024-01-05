namespace Customer.SupportHub.Application.Services.Cryptography;

public interface ICryptographyService : IApplicationInjection
{
	string EncryptPassword(string password);
	bool VerifyPassword(string password, string hashedPassword);
}