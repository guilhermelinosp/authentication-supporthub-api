using System.Security.Cryptography;
using System.Text;

namespace Company.SupportHub.Application.Services.Cryptography;

public class CryptographyService : ICryptographyService
{
	public string EncryptPassword(string password)
	{
		var salt = GenerateSalt();
		var hash = GenerateHash(password, salt);
		return $"{salt}.{hash}";
	}

	public bool VerifyPassword(string password, string hashedPassword)
	{
		var parts = hashedPassword.Split('.', 2);
		var salt = parts[0];
		var hash = parts[1];
		var hashedPasswordAttempt = GenerateHash(password, salt);
		return hash == hashedPasswordAttempt;
	}

	private string GenerateSalt()
	{
		var salt = new byte[16];
		using var random = RandomNumberGenerator.Create();
		random.GetBytes(salt);
		return Convert.ToBase64String(salt);
	}

	private string GenerateHash(string password, string salt)
	{
		var hashedInputBytes = SHA512.HashData(Encoding.UTF8.GetBytes(password + salt));
		return Convert.ToBase64String(hashedInputBytes);
	}
}