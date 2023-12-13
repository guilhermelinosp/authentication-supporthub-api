using FluentValidation;
using SupportHub.Auth.Domain.DTOs.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.Application.UseCases.Companies.Validators;

public class ForgotPasswordValidator : AbstractValidator<RequestForgotPassword>
{
    public ForgotPasswordValidator()
    {
        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage(MessagesException.EMAIL_NAO_INFORMADO)
            .EmailAddress()
            .WithMessage(MessagesException.EMAIL_INVALIDO);
    }
}