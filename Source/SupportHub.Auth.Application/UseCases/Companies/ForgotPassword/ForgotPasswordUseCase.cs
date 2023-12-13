using SupportHub.Auth.Application.UseCases.Companies.Validators;
using SupportHub.Auth.Domain.Cache;
using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.DTOs.Responses;
using SupportHub.Auth.Domain.Exceptions;
using SupportHub.Auth.Domain.Repositories;
using SupportHub.Auth.Domain.Services;

namespace SupportHub.Auth.Application.UseCases.Companies.ForgotPassword;

public class ForgotPasswordUseCase(
    ICompanyRepository repository,
    ISendGridService sendGridService,
    ITwilioService twilioService,
    IOneTimePasswordCache oneTimePassword)
    : IForgotPasswordUseCase
{
    public async Task<ResponseDefault> ExecuteAsync(RequestForgotPassword request)
    {
        var validatorRequest = await new ForgotPasswordValidator().ValidateAsync(request);
        if (!validatorRequest.IsValid)
            throw new DefaultException(validatorRequest.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await repository.FindCompanyByEmailAsync(request.Email);
        if (account is null)
            throw new DefaultException([MessagesException.EMAIL_NAO_ENCONTRADO]);
        
        var code = oneTimePassword.GenerateOneTimePassword(account.CompanyId.ToString());
        
        if (account.Is2Fa)
            await twilioService.SendSignInAsync(account.Phone, code);
        else
            await sendGridService.SendSignInAsync(request.Email, code);
        
        return new ResponseDefault(account.CompanyId.ToString(), MessagesResponse.CODIGO_ENVIADO);
    }
}