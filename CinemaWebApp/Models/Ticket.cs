using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // 👈 1. ВАЖЛИВО: Додали цю бібліотеку

namespace CinemaWebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Дата покупки")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [Display(Name = "Місце")]
        public int SeatNumber { get; set; }

        [Display(Name = "Ціна")]
        [Column(TypeName = "decimal(18, 2)")] // 👈 2. ВАЖЛИВО: Вказали формат грошей
        public decimal Price { get; set; }

        // --- Зв'язки ---

        public int SessionId { get; set; }
        public Session Session { get; set; }

        public string UserId { get; set; }
    }
}