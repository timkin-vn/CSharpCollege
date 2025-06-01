using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        public string Series { get; set; }

        /// <summary>
        /// Дата выпуска
        /// </summary>
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во на складе
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// Месяцев гарантии
        /// </summary>
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Дата прекращения производства
        /// </summary>
        public DateTime? DiscontinuedDate { get; set; }

        /// <summary>
        /// Страна-производитель
        /// </summary>
        public string ProducingCountry { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Category = Category,
                Manufacturer = Manufacturer,
                Series = Series,
                ReleaseDate = ReleaseDate,
                Price = Price,
                StockQuantity = StockQuantity,
                WarrantyMonths = WarrantyMonths,
                DiscontinuedDate = DiscontinuedDate,
                ProducingCountry = ProducingCountry,
            };
        }
    }
}
