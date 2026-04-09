using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        /// <summary>
        /// Id заметки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст заметки
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Выполнено
        /// </summary>
        public bool IsDone { get; set; }

        /// <summary>
        /// Закреплено
        /// </summary>
        public bool IsPinned { get; set; }
    }
}