using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.Application.UseCases.Companies.SignUp;

public partial class SignUpValidator : AbstractValidator<RequestSignUp>
{
    public SignUpValidator()
    {
        RuleFor(c => c.Cnpj)
            .NotEmpty()
            .WithMessage(MessagesException.CNPJ_NAO_INFORMADO)
            .Custom((cnpj, validator) =>
            {
                if (!RegexCnpj().IsMatch(cnpj))
                    validator.AddFailure(new ValidationFailure(nameof(RequestSignUp.Cnpj),
                        MessagesException.CNPJ_INVALIDO));
            });

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(MessagesException.EMAIL_NAO_INFORMADO)
            .EmailAddress()
            .WithMessage(MessagesException.EMAIL_INVALIDO);

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(MessagesException.SENHA_NAO_INFORMADO)
            .MinimumLength(8)
            .WithMessage(MessagesException.SENHA_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(MessagesException.SENHA_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                if (!RegexPassword().IsMatch(password))
                    validator.AddFailure(new ValidationFailure(nameof(RequestSignUp.Password),
                        MessagesException.SENHA_INVALIDA));
            });

        RuleFor(c => c.PasswordConfirmation)
            .NotEmpty()
            .WithMessage(MessagesException.SENHA_NAO_INFORMADO)
            .MinimumLength(8)
            .WithMessage(MessagesException.SENHA_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(MessagesException.SENHA_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                if (!RegexPassword().IsMatch(password))
                    validator.AddFailure(new ValidationFailure(nameof(RequestSignUp.Password),
                        MessagesException.SENHA_INVALIDA));
            });
    }


    [GeneratedRegex(@"^\d{14}$")]
    private static partial Regex RegexCnpj();

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
    private static partial Regex RegexPasswoed();
}