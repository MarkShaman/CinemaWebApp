using System;
using System.ComponentModel.DataAnnotations.Schema; 

namespace CinemaWebApp.Models
{
    public class Session
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        
        // Вказуємо тип decimal(18, 2) - це стандарт для грошей
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TicketPrice { get; set; }
        

        public int MovieId { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}