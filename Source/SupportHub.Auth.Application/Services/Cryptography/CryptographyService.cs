using System.Security.Cryptography;
using System.Text;

namespace SupportHub.Auth.Application.Services.Cryptography;

public class CryptographyService : ICryptographyService
{
    public string EncryptPassword(string password)
    {
        var salt = GenerateSalt();
        var hash = GenerateHash(password, salt);
        return $"{salt}.{hash}";
    }

    public string EncryptCode(string code)
    {
        var salt = GenerateSalt();
        var hash = GenerateHash(code, salt);
        return $"{salt}.{hash}";
    }

    public string EncryptEmail(string email)
    {
        var salt = GenerateSalt();
        var hash = GenerateHash(email, salt);
        return $"{salt}.{hash}";
    }

    public string EncryptPhone(string phone)
    {
        var salt = GenerateSalt();
        var hash = GenerateHash(phone, salt);
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