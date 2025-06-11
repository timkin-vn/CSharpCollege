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

        public string Name { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        public DateTime YearOfProduction { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Оклад
        /// </summary>
        public int Price { get; set; }

    }
}
