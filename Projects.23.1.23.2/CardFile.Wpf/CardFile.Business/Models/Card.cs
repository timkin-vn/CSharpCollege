using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
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
        /// Конструктор (КБ или личность)
        /// </summary>
        public string Designer { get; set; }

        /// <summary>
        /// Год начала производства
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Тип техники (танк, самолёт и т.д.)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Максимальная скорость (км/ч)
        /// </summary>
        public double MaxSpeed { get; set; }

        /// <summary>
        /// Характеристики орудия
        /// </summary>
        public string Gun { get; set; }

        /// <summary>
        /// Вес в тоннах/килограммах
        /// </summary>
        public double Weight { get; set; }
    }
}