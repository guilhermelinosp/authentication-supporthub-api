using System.Text.RegularExpressions;
using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace Customer.SupportHub.Application.UseCases.Validators;

public partial class ResetPasswordValidator : AbstractValidator<RequestResetPassword>
{
	public ResetPasswordValidator()
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
					validator.AddFailure(new ValidationFailure(
						nameof(RequestResetPassword.Password), MessageException.SENHA_INVALIDA));
			});
	}

	[GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
	private static partial Regex MyRegex();
}