using System.Text.RegularExpressions;
using Authentication.SupportHub.Domain.DTOs.Messages;
using Authentication.SupportHub.Domain.DTOs.Requests;
using FluentValidation;
using FluentValidation.Results;

namespace Authentication.SupportHub.Application.UseCases.Validators;

public partial class ValidatorSignUpAccount : AbstractValidator<RequestSignUpAccount>
{
	public ValidatorSignUpAccount()
	{
		RuleFor(c => c.Cnpj)
			.NotEmpty()
			.WithMessage(MessageException.IDENTITY_NAO_INFORMADO)
			.Custom((cnpj, validator) =>
			{
				if (!RegexCnpj().IsMatch(cnpj))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignUpAccount.Cnpj),
						MessageException.IDENTITY_INVALIDO));
			});

		RuleFor(c => c.Email)
			.NotEmpty()
			.WithMessage(MessageException.EMAIL_NAO_INFORMADO)
			.EmailAddress()
			.WithMessage(MessageException.EMAIL_INVALIDO);

		RuleFor(c => c.Password)
			.NotEmpty()
			.WithMessage(MessageException.SENHA_NAO_INFORMADO)
			.MinimumLength(8)
			.WithMessage(MessageException.SENHA_MINIMO_OITO_CARACTERES)
			.MaximumLength(16)
			.WithMessage(MessageException.SENHA_MAXIMO_DEZESSEIS_CARACTERES)
			.Custom((password, validator) =>
			{
				if (!RegexPassword().IsMatch(password))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignUpAccount.Password),
						MessageException.SENHA_INVALIDA));
			});

		RuleFor(c => c.PasswordConfirmation)
			.NotEmpty()
			.WithMessage(MessageException.SENHA_NAO_INFORMADO)
			.MinimumLength(8)
			.WithMessage(MessageException.SENHA_MINIMO_OITO_CARACTERES)
			.MaximumLength(16)
			.WithMessage(MessageException.SENHA_MAXIMO_DEZESSEIS_CARACTERES)
			.Custom((password, validator) =>
			{
				if (!RegexPassword().IsMatch(password))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignUpAccount.Password),
						MessageException.SENHA_INVALIDA));
			});
	}


	[GeneratedRegex(@"^\d{14}$")]
	private static partial Regex RegexCnpj();

	[GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
	private static partial Regex RegexPassword();
}