using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp.Confirmation;

public class ConfirmationSignUpUseCase(ICompanyRepository repository) : IConfirmationSignUpUseCase
{
    public async Task ExecuteAsync(string code)
    {
        var validatorRequest = await new ConfirmationSignUpValidator().ValidateAsync(code.ToUpper());
        if (!validatorRequest.IsValid)
            throw new ValidatorException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByCodeAsync(code.ToUpper());
        if (account is null) 
            throw new CompanyException(new List<string> { MessagesException.CODIGO_INVALIDO });
        
        if (account.IsVerified)
            throw new CompanyException(new List<string> { MessagesException.EMAIL_JA_VERIFICADO });

        if (account.Code != code.ToUpper())
            throw new CompanyException(new List<string> { MessagesException.CODIGO_INVALIDO });

        account.IsVerified = true;
        
        account.Code = string.Empty;

        await repository.UpdateCompanyAsync(account);
    }
}