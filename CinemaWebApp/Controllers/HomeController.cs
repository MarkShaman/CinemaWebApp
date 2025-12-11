using CinemaWebApp.Data;
using CinemaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CinemaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // Додали контекст бази

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context; // Ініціалізуємо його
        }

        
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                
                var movie = await _context.Movie         
                    .Include(m => m.Sessions)             
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null)
                {
                    return NotFound();
                }

                return View(movie);
            }
            
            public async Task<IActionResult> Index()
        {
            // Беремо всі фільми з бази і передаємо їх на сторінку
            var movies = await _context.Movie.ToListAsync();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}