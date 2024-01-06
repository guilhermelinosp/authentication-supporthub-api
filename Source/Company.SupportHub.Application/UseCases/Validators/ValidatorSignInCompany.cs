using System.Text.RegularExpressions;
using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.Messages;
using FluentValidation;
using FluentValidation.Results;

namespace Company.SupportHub.Application.UseCases.Validators;

public partial class ValidatorSignInCompany : AbstractValidator<RequestSignInCompany>
{
	public ValidatorSignInCompany()
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
				if (!MyRegex().IsMatch(password))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignInEmployee.Password),
						MessageException.SENHA_INVALIDA));
			});

		RuleFor(c => c.Cnpj)
			.NotEmpty()
			.WithMessage(MessageException.CNPJ_NAO_INFORMADO)
			.Custom((cnpj, validator) =>
			{
				if (!RegexCnpj().IsMatch(cnpj))
					validator.AddFailure(new ValidationFailure(nameof(RequestSignUp.Cnpj),
						MessageException.CNPJ_INVALIDO));
			});
	}

	[GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
	private static partial Regex MyRegex();

	[GeneratedRegex(@"^\d{14}$")]
	private static partial Regex RegexCnpj();
}