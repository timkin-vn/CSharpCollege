using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }       // Название
        public string Genre { get; set; }       // Жанр
        public double GlobalRating { get; set; } // Мировой рейтинг
        public int MyScore { get; set; }        // Личная оценка (1-10)
        public DateTime ReleaseDate { get; set; } // Дата выхода
        public string Description { get; set; } // Краткое описание (по желанию)
    }
}
