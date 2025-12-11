using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Display(Name = "Назва фільму")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Опис")]
        public string? Description { get; set; }

        [Display(Name = "Тривалість (хв)")]
        public int Duration { get; set; }

        [Display(Name = "Рік випуску")]
        public int ReleaseYear { get; set; } // Увага: було Year, тут ReleaseYear

        // --- Нові поля для курсової ---

        [Display(Name = "Постер")]
        public string? ImageUrl { get; set; } // Посилання на картинку

        [Display(Name = "Режисер")]
        public string? Director { get; set; }

        [Display(Name = "Жанр")]
        public string? Genre { get; set; }
 
        public List<Session> Sessions { get; set; } = new List<Session>();
    }
}