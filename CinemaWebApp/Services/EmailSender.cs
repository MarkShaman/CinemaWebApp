using Microsoft.AspNetCore.Identity.UI.Services;

namespace CinemaWebApp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Тут поки що порожньо. Ми просто обманюємо систему, 
            // вдаючи, що лист відправлено.
            return Task.CompletedTask;
        }
    }
}