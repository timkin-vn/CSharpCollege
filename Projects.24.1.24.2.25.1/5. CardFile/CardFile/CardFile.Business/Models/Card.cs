using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отрасль
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// Год основания
        /// </summary>
        public int FoundationYear { get; set; }

        /// <summary>
        /// Выручка (млрд руб.)
        /// </summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// Число сотрудников
        /// </summary>
        public int Employees { get; set; }

        /// <summary>
        /// Информация о компании
        /// </summary>
        public string Description { get; set; }
    }
}