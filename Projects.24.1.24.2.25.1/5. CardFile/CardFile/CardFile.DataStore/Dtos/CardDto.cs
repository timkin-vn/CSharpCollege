using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }        // Название фильма
        public string Director { get; set; }     // Режиссер
        public int Year { get; set; }             // Год выпуска
        public string Genre { get; set; }        // Жанр
        public int Duration { get; set; }        // Длительность (мин)
        public decimal Rating { get; set; }       // Рейтинг (например, 8.4)

        public CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
