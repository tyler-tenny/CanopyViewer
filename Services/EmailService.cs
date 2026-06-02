using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore.Metadata;
using MimeKit;

namespace CanopyViewer.Services
{
    //This service handles sending email notifications for new work orders to configured users
    public class EmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendWorkOrderNotificationAsync(
            string toEmail, string toName, string workOrderTitle, int workOrderId)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    _config["Email:SenderName"],
                    _config["Email:SenderAddress"]));
                message.To.Add(new MailboxAddress(toName, toEmail));
                message.Subject = $"New Work Order Created: {workOrderTitle}";

                message.Body = new TextPart("html")
                {
                    Text = $"""
                        <h3>A new work order has been created.</h3>
                        <p><strong>Title:</strong> {workOrderTitle}<p>
                        <p><strong>Work Order ID:</strong> {workOrderId}<p>
                        <p>Log in to CanopyViewer to view details.</p>
                    """
                };

                using var client = new SmtpClient();
                await client.ConnectAsync(
                    _config["Email:SmtpHost"],
                    int.Parse(_config["Email:SmtpPort"]!),
                    SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(
                    _config["Email:Username"],
                    _config["Email:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", toEmail);
            }
        }
    }
}
