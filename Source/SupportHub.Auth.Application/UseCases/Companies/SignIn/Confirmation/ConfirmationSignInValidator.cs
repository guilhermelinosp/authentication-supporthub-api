using FluentValidation;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.Application.UseCases.Companies.SignIn.Confirmation;

public class ConfirmationSignInValidator : AbstractValidator<string>
{
    public ConfirmationSignInValidator()
    {
        RuleFor(s => s)
            .NotEmpty()
            .WithMessage(MessagesException.CODIGO_INVALIDO)
            .MinimumLength(6)
            .WithMessage(MessagesException.CODIGO_INVALIDO)
            .MaximumLength(6)
            .WithMessage(MessagesException.CODIGO_INVALIDO);
    }
}