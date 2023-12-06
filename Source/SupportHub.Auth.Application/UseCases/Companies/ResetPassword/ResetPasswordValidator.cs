using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using SupportHub.Auth.Domain.Dtos.Requests.Companies;
using SupportHub.Auth.Domain.Exceptions;

namespace SupportHub.Auth.Application.UseCases.Companies.ResetPassword;

public partial class ResetPasswordValidator : AbstractValidator<RequestResetPassword>
{
    public ResetPasswordValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(MessagesException.SENHA_NAO_INFORMADO)
            .MinimumLength(8)
            .WithMessage(MessagesException.SENHA_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(MessagesException.SENHA_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                if (!MyRegex().IsMatch(password))
                    validator.AddFailure(new ValidationFailure(
                        nameof(RequestResetPassword.Password), MessagesException.SENHA_INVALIDA));
            });
    }

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
    private static partial Regex MyRegex();
}