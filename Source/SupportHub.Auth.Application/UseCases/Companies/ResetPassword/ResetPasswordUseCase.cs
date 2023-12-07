using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;

namespace SupportHub.Auth.Application.UseCases.Companies.ResetPassword;

public class ResetPasswordUseCase(ICompanyRepository repository, IEncryptService encrypt)
    : IResetPasswordUseCase
{
    public async Task ExecuteAsync(RequestResetPassword request, string code)
    {
        var validatorRequest = await new ResetPasswordValidator().ValidateAsync(request);
        if (!validatorRequest.IsValid)
            throw new ValidatorException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByCodeAsync(code);
        if (account is null)
            throw new CompanyException(new List<string> { MessagesException.CODIGO_INVALIDO });

        if (request.Password != request.PasswordConfirmation)
            throw new CompanyException(new List<string> { MessagesException.SENHA_NAO_CONFERE });

        account.Password = encrypt.EncryptPassword(request.Password!);

        account.Code = string.Empty;

        await repository.UpdateCompanyAsync(account);
    }
}