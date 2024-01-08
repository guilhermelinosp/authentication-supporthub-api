using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using Authentication.SupportHub.Domain.Services;

namespace Authentication.SupportHub.Infrastructure.Services;

public class SendGridService(IConfiguration configuration) : ISendGridService, IInfrastructureInjection
{
	public async Task SendSignUpAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid:ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid:FromEmail"]!, configuration["SendGrid:FromName"]!);
		var to = new EmailAddress(email);
		const string subject = "Verify Your Email - Support Hub";
		const string plainTextContent = "Verify Your Email - Support Hub";

		var htmlContent = $@"
        <p>Thank you for signing up with Support Hub!</p>

        <p>To complete your registration, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not create an account with Support Hub or have any concerns, please contact our support team at {configuration["SendGrid:FromEmail"]}.</p>

        <p>Welcome aboard, and thank you for choosing Support Hub!</p>

        <p>Best regards,</p>
        <p>The Team at Support Hub</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}

	public async Task SendForgotPasswordAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid:ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid:FromEmail"]!, configuration["SendGrid:FromName"]!);
		var to = new EmailAddress(email);
		const string subject = "Reset Your Password - Support Hub";
		const string plainTextContent = "Reset Your Password - Support Hub";

		var htmlContent = $@"
        <p>We received a request to reset the password for your Support Hub account.</p>

        <p>If you initiated this request, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not request a password reset or have any concerns, please contact our support team at {configuration["SendGrid:FromEmail"]}.</p>

        <p>Thank you for choosing Test API. Your account security is important to us!</p>

        <p>Best regards,</p>
        <p>The Team at Support Hub</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}

	public async Task SendSignInAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid:ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid:FromEmail"]!, configuration["SendGrid:FromName"]!);
		var to = new EmailAddress(email);
		const string subject = "Sign In Verification - Support Hub";
		const string plainTextContent = "Sign In Verification - Support Hub";

		var htmlContent = $@"
        <p>We have received a request to sign in to your Support Hub account.</p>

        <p>If you initiated this sign-in, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not initiate this sign-in or have any concerns, please contact our support team at {configuration["SendGrid:FromEmail"]}.</p>

        <p>Thank you for choosing Suppot Hub. We appreciate your security commitment!</p>

        <p>Best regards,</p>
        <p>The Team at Support Hub</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}
}