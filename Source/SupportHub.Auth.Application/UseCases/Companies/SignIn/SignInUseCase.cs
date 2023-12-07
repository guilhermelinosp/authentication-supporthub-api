using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.ServicesExternal;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn;

public class SignInUseCase(
    ICompanyRepository repository,
    IEncryptService encryptService,
    ISendGrid sendGrid,
    ITwilio twilio)
    : ISignInUseCase
{
    public async Task ExecuteAsync(RequestSignInEmail request)
    {
        var validator = await new SignInValidator().ValidateAsync(request);
        if (!validator.IsValid)
            throw new ValidatorException(validator.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByEmailAsync(request.Email);
        if (account is null)
            throw new CompanyException(new List<string> { MessagesException.EMAIL_NAO_ENCONTRADO });

        if (!encryptService.VerifyPassword(request.Password, account.Password))
            throw new CompanyException(new List<string> { MessagesException.SENHA_INVALIDA });

        if (!account.IsVerified)
            throw new CompanyException(new List<string> { MessagesException.EMAIL_NAO_AUTENTICADO });

        var code = encryptService.GenerateCode().ToUpper();

        account.Code = code;

        await repository.UpdateCompanyAsync(account);

        if (account.Is2Fa)
            await twilio.SendSignInAsync(account.Phone, code);
        else
            await sendGrid.SendSignInAsync(request.Email, code);
    }
}