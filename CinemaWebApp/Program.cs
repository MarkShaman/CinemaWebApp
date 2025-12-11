using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services; // Потрібно для інтерфейсу пошти
using Microsoft.EntityFrameworkCore;
using CinemaWebApp.Data;
using CinemaWebApp.Models;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// 1. Підключення до бази даних
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")
    ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));

// 2. Налаштування Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// --- ВИРІШЕННЯ ПРОБЛЕМИ ---
// Реєструємо наш "внутрішній" клас відправки пошти
builder.Services.AddTransient<IEmailSender, InternalEmailSender>();
// ---------------------------

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

var defaultDateCulture = "uk-UA"; // Українська культура
var ci = new CultureInfo(defaultDateCulture);

// Примусово ставимо крапку як роздільник для чисел (щоб не сварився браузер)
ci.NumberFormat.NumberDecimalSeparator = ".";
ci.NumberFormat.CurrencyDecimalSeparator = ".";

// Налаштовуємо локалізацію
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ci),
    SupportedCultures = new List<CultureInfo> { ci },
    SupportedUICultures = new List<CultureInfo> { ci }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
CinemaWebApp.Data.AppDbInitializer.Seed(app);

app.Run();

// 👇 МИ ДОДАЛИ КЛАС ПРЯМО СЮДИ, В КІНЕЦЬ ФАЙЛУ 👇
// Це гарантує, що програма його точно знайде
public class InternalEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Просто імітуємо відправку
        return Task.CompletedTask;
    }
}