using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// Место хранения
        /// </summary>
        public string StorageLocation { get; set; }

        /// <summary>
        /// Дата последнего пополнения
        /// </summary>
        public DateTime LastRestockDate { get; set; }

        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
