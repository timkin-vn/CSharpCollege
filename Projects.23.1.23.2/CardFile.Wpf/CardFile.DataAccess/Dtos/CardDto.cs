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
        /// Название техники (например, "Т-34")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Производитель/страна (например, "СССР")
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Конструктор (например, "КБ Морозова" или "Михаил Кошкин")
        /// </summary>
        public string Designer { get; set; }

        /// <summary>
        /// Год начала производства (например, 1940)
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Тип техники (например, "Танк", "Истребитель")
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Максимальная скорость (в км/ч)
        /// </summary>
        public double MaxSpeed { get; set; }

        /// <summary>
        /// Калибр/модель орудия (например, "76-мм Ф-34")
        /// </summary>
        public string Gun { get; set; }

        /// <summary>
        /// Вес в тоннах/килограммах (например, 26.5)
        /// </summary>
        public double Weight { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Name = Name,
                Manufacturer = Manufacturer,
                Designer = Designer,
                ProductionYear = ProductionYear,
                Type = Type,
                MaxSpeed = MaxSpeed,
                Gun = Gun,
                Weight = Weight
            };
        }
    }
}
