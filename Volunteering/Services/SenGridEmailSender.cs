using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Volunteering.Services
{
    /*public class SenGridEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        //private readonly ILogger _logger;
        public SenGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = _configuration["SendGridAPI"];
            var client = new SendGridClient(apiKey);
            var message = new SendGridMessage()
            {
                From = new EmailAddress("tapasz1488@gmail.com", "Taras"),
                Subject = subject,
                PlainTextContent = htmlMessage,
                HtmlContent = htmlMessage
            };
            message.AddTo(email);

            var response = await client.SendEmailAsync(message);
            int x = 0;
        }
    }*/
}
