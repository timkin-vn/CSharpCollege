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
        // DTO для хранения данных о фильме или игре

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Мировой рейтинг (например, IMDB)
        /// </summary>
        public double GlobalRating { get; set; }

        /// <summary>
        /// Моя личная оценка (1-10)
        /// </summary>
        public int MyScore { get; set; }

        /// <summary>
        /// Дата выхода
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Описание или заметки
        /// </summary>
        public string Description { get; set; }
        public CardDto Clone()
        {
            // AutoMapper подхватит новые поля автоматически при клонировании
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}

