using Authentication.SupportHub.Domain.DTOs.Requests;
using Authentication.SupportHub.Domain.Messages;
using FluentValidation;

namespace Authentication.SupportHub.Application.UseCases.Validators;

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