using Company.SupportHub.Domain.DTOs.Requests;
using Company.SupportHub.Domain.Exceptions;
using Company.SupportHub.Domain.Messages;
using FluentValidation;

namespace Company.SupportHub.Application.UseCases.Validators;

public class ValidatorForgotPassword : AbstractValidator<RequestForgotPassword>
{
	public ValidatorForgotPassword()
	{
		RuleFor(e => e.Email)
			.NotEmpty()
			.WithMessage(MessageException.EMAIL_NAO_INFORMADO)
			.EmailAddress()
			.WithMessage(MessageException.EMAIL_INVALIDO);
	}
}