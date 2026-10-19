using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        [XmlAttribute("ProductName")]
        /// <summary>
        /// Название товара
        /// </summary>
        public string ProductName { get; set; }

        [XmlAttribute("Category")]
        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        [XmlAttribute("Brand")]
        /// <summary>
        /// Бренд
        /// </summary>
        public string Brand { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime DateAdded { get; set; }

        [XmlAttribute("DateAdded")]
        public string DateAddedText
        {
            get => DateAdded.ToShortDateString();
            set => DateAdded = DateTime.Parse(value);
        }

        [XmlAttribute("Supplier")]
        /// <summary>
        /// Поставщик
        /// </summary>
        public string Supplier { get; set; }

        [XmlAttribute("StorageLocation")]
        /// <summary>
        /// Место хранения
        /// </summary>
        public string StorageLocation { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Дата последнего пополнения
        /// </summary>
        public DateTime LastRestockDate { get; set; }

        [XmlAttribute("LastRestockDate")]
        public string LastRestockDateText
        {
            get => LastRestockDate.ToShortDateString();
            set => LastRestockDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        /// <summary>
        /// Срок годности
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        [XmlAttribute("ExpirationDate")]
        public string ExpirationDateText
        {
            get => ExpirationDate?.ToShortDateString() ?? "-";
            set => ExpirationDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Price")]
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}
