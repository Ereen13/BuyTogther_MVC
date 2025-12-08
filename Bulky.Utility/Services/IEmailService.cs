using BulkyBook.Models;

namespace BulkyBook.Utility
{
    public interface IEmailService
    {
        //Task SendOrderConfirmationAsync(OrderHeader order);
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}