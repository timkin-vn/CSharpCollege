using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Краткое описание
        /// </summary>
        public string Description { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Title = Title,
                Author = Author,
                Year = Year,
                Genre = Genre,
                Description = Description
            };
        }
    }
}
