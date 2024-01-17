using System.Text.RegularExpressions;
using Authentication.SupportHub.Domain.DTOs.Messages;
using Authentication.SupportHub.Domain.DTOs.Requests;
using FluentValidation;
using FluentValidation.Results;

namespace Authentication.SupportHub.Application.UseCases.Validators;

public partial class ValidatorSignInAccount : AbstractValidator<RequestSignInAccount>
{
	public ValidatorSignInAccount()
	{
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
					validator.AddFailure(new ValidationFailure(nameof(RequestSignInAccount.Password),
						MessageException.SENHA_INVALIDA));
			});

		RuleFor(c => c.Identity)
			.NotEmpty()
			.WithMessage(MessageException.IDENTITY_NAO_INFORMADO)
			.MaximumLength(14)
			.WithMessage(MessageException.IDENTITY_MAXIMO_QUATORZE_CARACTERES)
			.MinimumLength(11)
			.WithMessage(MessageException.IDENTITY_MINIMO_ONZE_CARACTERES)
			.Custom((cnpj, validator) =>
			{
				if (!RegexIdentity().IsMatch(cnpj))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignInAccount.Identity),
						MessageException.IDENTITY_INVALIDO));
			});
	}

	[GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
	private static partial Regex RegexPassword();

	[GeneratedRegex(@"^\d+$")]
	private static partial Regex RegexIdentity();
}