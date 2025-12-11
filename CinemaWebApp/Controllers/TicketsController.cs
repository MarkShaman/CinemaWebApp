using CinemaWebApp.Data;
using CinemaWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Controllers
{
    [Authorize] 
    public class TicketsController : Controller
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        // 1. Показати сторінку підтвердження (GET)
        [HttpGet]
        public async Task<IActionResult> Create(int sessionId)
        {
            var session = await _context.Sessions
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.Id == sessionId);

            if (session == null) return NotFound();

            return View(session);
        }
        // Цей метод покаже список квитків користувача
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Дізнаємося, хто зараз на сайті (email або логін)
            var userId = User.Identity?.Name ?? "Anonim";

            var tickets = await _context.Tickets
                .Include(t => t.Session)          // Підвантажуємо дані про сеанс
                .ThenInclude(s => s.Movie)        // І про фільм (щоб знати назву)
                .Where(t => t.UserId == userId)   // Фільтруємо: ТІЛЬКИ мої квитки
                .OrderByDescending(t => t.PurchaseDate) // Спочатку нові
                .ToListAsync();

            return View(tickets);
        }

        // 2. Зберегти квиток у базу (POST)
        [HttpPost]
        public async Task<IActionResult> ConfirmCreate(int sessionId)
        {
            var session = await _context.Sessions.FindAsync(sessionId);
            if (session == null) return NotFound();

            // Хто купує? (Беремо email поточного користувача)
            var userId = User.Identity?.Name ?? "Anonim";

            var ticket = new Ticket
            {
                SessionId = sessionId,
                UserId = userId,
                Price = session.TicketPrice,
                SeatNumber = new Random().Next(1, 100), // Генеруємо випадкове місце (для спрощення)
                PurchaseDate = DateTime.Now
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Перенаправляємо на сторінку подяки або назад
            return RedirectToAction("Index", "Home");
        }
    }
}