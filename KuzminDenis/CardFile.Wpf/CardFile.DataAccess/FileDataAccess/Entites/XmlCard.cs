using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        [XmlAttribute("Category")]
        public string Category { get; set; }

        /// <summary>
        /// Производитель
        /// </summary>
        [XmlAttribute("Manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Серия
        /// </summary>
        [XmlAttribute("Series")]
        public string Series { get; set; }

        /// <summary>
        /// Дата выпуска
        /// </summary>
        [XmlIgnore]
        public DateTime ReleaseDate { get; set; }

        [XmlAttribute("ReleaseDate")]
        public string ReleaseDateXml
        {
            get => ReleaseDate.ToShortDateString();
            set => ReleaseDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Цена
        /// </summary>
        [XmlAttribute("Price")]
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во на складе
        /// </summary>
        [XmlAttribute("StockQuantity")]
        public int StockQuantity { get; set; }

        /// <summary>
        /// Месяцев гарантии
        /// </summary>
        [XmlAttribute("WarrantyMonths")]
        public int WarrantyMonths { get; set; }

        /// <summary>
        /// Дата прекращения производства
        /// </summary>
        [XmlIgnore]
        public DateTime? DiscontinuedDate { get; set; }

        [XmlAttribute("DiscontinuedDate")]
        public string DiscontinuedDateXml
        {
            get => DiscontinuedDate.HasValue ? DiscontinuedDate.Value.ToShortDateString() : "-";
            set => DiscontinuedDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Страна-производитель
        /// </summary>
        [XmlAttribute("ProducingCountry")]
        public string ProducingCountry { get; set; }
    }
}
