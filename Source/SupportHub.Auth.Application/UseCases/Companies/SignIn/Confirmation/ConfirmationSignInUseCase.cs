using System.Globalization;
using Microsoft.Extensions.Configuration;
using SupportHub.Auth.Application.Services.Tokenization;
using SupportHub.Auth.Domain.Dtos.Responses.Companies;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public class ConfirmationSignInUseCase(
    ICompanyRepository repository,
    ITokenService tokenService,
    IConfiguration configuration
    ) : IConfirmationSignInUseCase
{
    public async Task<ResponseSignIn> ExecuteAsync(string code)
    {
        var validatorRequest = await new ConfirmationSignInValidator().ValidateAsync(code.ToUpper());
        if (!validatorRequest.IsValid)
            throw new ValidatorException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByCodeAsync(code.ToUpper());
        if (account is null) 
            throw new CompanyException(new List<string> { MessagesException.CODIGO_INVALIDO });

        if (account.Code != code.ToUpper())
            throw new CompanyException(new List<string> { MessagesException.CODIGO_INVALIDO });
        
        account.Code = string.Empty;

        await repository.UpdateCompanyAsync(account);

        return new ResponseSignIn
        {
            Token = tokenService.GenerateToken(account.CompanyId.ToString()),
            RefreshToken = tokenService.GenerateRefreshToken(),
            ExpiryDate =
                DateTime.UtcNow.Add(TimeSpan.Parse(configuration["Jwt:Expiry"]!, CultureInfo.InvariantCulture))
        };
    }
}