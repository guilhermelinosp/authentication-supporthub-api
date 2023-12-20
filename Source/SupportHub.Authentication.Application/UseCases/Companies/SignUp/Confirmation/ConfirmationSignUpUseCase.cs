using SupportHub.Authentication.Domain.Cache;
using SupportHub.Authentication.Domain.DTOs.Responses;
using SupportHub.Authentication.Domain.Exceptions;
using SupportHub.Authentication.Domain.Repositories;

namespace SupportHub.Authentication.Application.UseCases.Companies.SignUp.Confirmation;

public class ConfirmationSignUpUseCase(ICompanyRepository repository, IOneTimePasswordCache oneTimePassword) : IConfirmationSignUpUseCase
{
    public async Task<ResponseDefault> ExecuteAsync(string accountId, string code)
    {
        var validatorCode = oneTimePassword.ValidateOneTimePassword(accountId, code);
        if (!validatorCode)
            throw new DefaultException([MessagesException.CODIGO_INVALIDO]);
        
        var account = await repository.FindCompanyByIdAsync(Guid.Parse(accountId));
        if (account is null)
            throw new DefaultException([MessagesException.CONTA_NAO_ENCONTRADA]);

        account.IsVerified = true;
        
        await repository.UpdateCompanyAsync(account);
        
        return new ResponseDefault(accountId, MessagesResponse.CODIGO_CONFIRMADO);
    }
}