namespace SupportHub.Auth.Application.Services.Cryptography;

public interface IEncryptService
{
    string EncryptPassword(string password);
    string EncryptCode(string code);
    string EncryptEmail(string email);
    string EncryptPhone(string phone);
    bool VerifyPassword(string password, string hashedPassword);
    string GenerateCode();
}