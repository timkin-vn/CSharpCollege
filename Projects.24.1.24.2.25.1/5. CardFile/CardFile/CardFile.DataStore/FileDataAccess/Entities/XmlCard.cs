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
        public int Id { get; set; }

        [XmlAttribute("ProductName")]
        public string ProductName { get; set; }

        [XmlAttribute("ProductModel")]
        public string ProductModel { get; set; }

        [XmlAttribute("ProductColor")]
        public string ProductColor { get; set; }

        [XmlIgnore]
        public DateTime ManufactureDate { get; set; }
        [XmlAttribute("ManufactureDate")]
        public string ManufactureDateText
        {
            get => ManufactureDate.ToShortDateString();
            set => ManufactureDate = DateTime.Parse(value);
        }

        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlAttribute("Manufacturer")]
        public string Manufacturer { get; set; }

        [XmlIgnore]
        public DateTime ReceiptDate { get; set; }
        [XmlAttribute("ReceiptDate")]
        public string ReceiptDateText
        {
            get => ReceiptDate.ToShortDateString();
            set => ReceiptDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? WriteOffDate { get; set; }
        [XmlAttribute("WriteOffDate")]
        public string WriteOffDateText
        {
            get => WriteOffDate?.ToShortDateString() ?? "-";
            set => WriteOffDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}