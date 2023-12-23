using Customer.SupportHub.Domain.DTOs.Requests;
using Customer.SupportHub.Domain.Exceptions;
using FluentValidation;

namespace Customer.SupportHub.Application.UseCases.Validators;

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