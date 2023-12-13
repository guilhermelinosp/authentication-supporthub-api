using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Application.UseCases.Companies.Validators;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.Services;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn;

public class SignInUseCase(
    ICompanyRepository repository,
    ICryptographyService cryptographyService,
    ISendGridService sendGridService,
    ITwilioService twilioService,
    ISessionCache sessionCache,
    IOneTimePasswordCache oneTimePassword)
    : ISignInUseCase
{
    public async Task<ResponseDefault> ExecuteAsync(RequestSignIn request)
    {
        var validator = await new SignInValidator().ValidateAsync(request);
        if (!validator.IsValid)
            throw new DefaultException(validator.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByEmailAsync(request.Email);
        if (account is null)
            throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);

        if (!cryptographyService.VerifyPassword(request.Password, account.Password))
            throw new DefaultException([MessagesException.SENHA_INVALIDA]);
        
        var session = sessionCache.ValidateSessionAsync(account.CompanyId.ToString());
        if (session)
            throw new DefaultException([MessagesException.SESSION_ATIVA]);
        
        var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());

        if (!account.IsVerified)
        {
            await sendGridService.SendSignUpAsync(request.Email, code);
            throw new DefaultException([MessagesException.EMAIL_NAO_AUTENTICADO]);
        }
        
        await repository.UpdateCompanyAsync(account);

        if (account.Is2Fa)
            await twilioService.SendSignInAsync(account.Phone, code);
        else
            await sendGridService.SendSignInAsync(request.Email, code);

        return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
    }
}