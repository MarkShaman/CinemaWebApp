using Microsoft.AspNetCore.Identity;

namespace CinemaWebApp.Models // <-- Замініть на назву вашого проєкту (наприклад, WebApplication1.Models)
{
    // Ми наслідуємось від IdentityUser, щоб отримати готові поля (Email, PasswordHash, Id і т.д.)
    public class AppUser : IdentityUser
    {
        // Сюди пізніше можна додати власні поля, наприклад:
        // public string FirstName { get; set; }
    }
}