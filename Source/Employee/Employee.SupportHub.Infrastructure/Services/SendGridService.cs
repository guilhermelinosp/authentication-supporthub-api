using Employee.SupportHub.Domain.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Employee.SupportHub.Infrastructure.Services;

public class SendGridService(IConfiguration configuration) : ISendGridService
{
	public async Task SendSignUpAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid_ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid_FromEmail"]!);
		var to = new EmailAddress(email);
		const string subject = "Welcome to Test API - Verify Your Email";
		const string plainTextContent = "Welcome to Test API - Verify Your Email";

		var htmlContent = $@"
        <p>Thank you for signing up with Test API!</p>

        <p>To complete your registration, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not create an account with Test API or have any concerns, please contact our support team at {configuration["SendGrid_FromEmail"]}.</p>

        <p>Welcome aboard, and thank you for choosing Test API!</p>

        <p>Best regards,</p>
        <p>The Team at Test API</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}

	public async Task SendForgotPasswordAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid_ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid_FromEmail"]!);
		var to = new EmailAddress(email);
		const string subject = "Reset Your Password - Test API";
		const string plainTextContent = "Reset Your Password - Test API";

		var htmlContent = $@"
        <p>We received a request to reset the password for your Test API account.</p>

        <p>If you initiated this request, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not request a password reset or have any concerns, please contact our support team at {configuration["SendGrid_FromEmail"]}.</p>

        <p>Thank you for choosing Test API. Your account security is important to us!</p>

        <p>Best regards,</p>
        <p>The Team at Test API</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}

	public async Task SendSignInAsync(string email, string code)
	{
		var sendGridClient = new SendGridClient(configuration["SendGrid_ApiKey"]!);
		var from = new EmailAddress(configuration["SendGrid_FromEmail"]!);
		var to = new EmailAddress(email);
		const string subject = "Sign In Verification - Test API";
		const string plainTextContent = "Sign In Verification - Test API";

		var htmlContent = $@"
        <p>We have received a request to sign in to your Test API account.</p>

        <p>If you initiated this sign-in, please use the following verification code:</p>

        <p><strong>{code}</strong></p>

        <p>If you did not initiate this sign-in or have any concerns, please contact our support team at {configuration["SendGrid_FromEmail"]}.</p>

        <p>Thank you for choosing Test API. We appreciate your security commitment!</p>

        <p>Best regards,</p>
        <p>The Team at Test API</p>
    ";

		await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
			htmlContent));
	}
}