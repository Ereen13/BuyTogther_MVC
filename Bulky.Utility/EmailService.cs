using Resend;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BulkyBook.Utility
{
    public class EmailService : IEmailService, IEmailSender
    {
        private readonly IConfiguration _config;
        
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // Simple console logging
            Console.WriteLine($"Attempting to send email to {to}");
            
            var apiKey = _config["Resend:ApiKey"];
            
            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("ERROR: Resend API key is missing!");
                return;
            }
            
            try
            {
                IResend resend = ResendClient.Create(apiKey);
                
                await resend.EmailSendAsync(new()
                {
                    From = "GroupBy <onboarding@resend.dev>",
                    To = new EmailAddressList { to },
                    Subject = subject,
                    HtmlBody = body
                });
                
                Console.WriteLine($"SUCCESS: Email sent to {to}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR sending email to {to}: {ex.Message}");
                // Don't throw - just log and continue
            }
        }
       
    }
}