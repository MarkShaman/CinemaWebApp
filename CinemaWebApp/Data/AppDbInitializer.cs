using CinemaWebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CinemaWebApp.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // Перевіряємо, чи є фільми, якщо ні - додаємо
                if (!context.Movie.Any())
                {
                    context.Movie.AddRange(new Movie()
                    {
                        Title = "Dune",
                        Description = "Epic sci-fi",
                        
                    });
                    context.SaveChanges();
                }

                // Перевіряємо, чи є сеанси, якщо ні - додаємо
                if (!context.Sessions.Any())
                {
                    // Беремо ID першого фільму, який точно є в базі
                    var movie = context.Movie.FirstOrDefault();

                    context.Sessions.AddRange(new Session()
                    {
                        StartTime = DateTime.Now.AddDays(1).AddHours(5), // Завтра
                        TicketPrice = 150,
                        MovieId = movie.Id // Автоматично беремо правильний ID
                    },
                    new Session()
                    {
                        StartTime = DateTime.Now.AddDays(2).AddHours(2), // Післязавтра
                        TicketPrice = 200,
                        MovieId = movie.Id
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}