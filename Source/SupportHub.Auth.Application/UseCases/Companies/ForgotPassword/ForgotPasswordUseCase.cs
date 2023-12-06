using SupportHub.Auth.Application.Services.Cryptography;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.ServicesExternal;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;

public class ForgotPasswordUseCase(
        ICompanyRepository repository, 
        IEncryptService encrypt, 
        ISendGrid sendGrid)
    : IForgotPasswordUseCase
{
    public async Task ExecuteAsync(RequestForgotPassword request)
    {
        var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
        if (!validatorRequest.IsValid)
            throw new ValidatorException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByEmailAsync(request.Email);
        if (account is null)
            throw new CompanyException(new List<string> { MessagesException.EMAIL_NAO_ENCONTRADO });

        var code = encrypt.GenerateCode().ToUpper();

        account.Code = code;

        await repository.UpdateCompanyAsync(account);

        await sendGrid.SendForgotPasswordAsync(request.Email, code);
    }
}