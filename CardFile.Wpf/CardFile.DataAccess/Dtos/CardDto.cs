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

        public string Name { get; set; }

        /// <summary>
        /// год производства
        /// </summary>
        public DateTime YearOfProduction { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дата поставки
        /// </summary>
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>`
        public int Price { get; set; }


        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Name = Name,
                YearOfProduction = YearOfProduction,
                Type = Type,
                DeliveryDate = DeliveryDate,
                Price = Price
            };
        }
    }
}
