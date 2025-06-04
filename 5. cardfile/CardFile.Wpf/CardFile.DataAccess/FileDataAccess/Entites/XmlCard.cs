using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")] public int Id { get; set; }
        [XmlAttribute("ProductName")] public string ProductName { get; set; }
        [XmlAttribute("Category")] public string Category { get; set; }
        [XmlAttribute("Manufacturer")] public string Manufacturer { get; set; }

        [XmlIgnore] public DateTime ProductionDate { get; set; }
        [XmlAttribute("ProductionDate")]
        public string ProductionDateXml
        {
            get => ProductionDate.ToShortDateString();
            set => ProductionDate = DateTime.Parse(value);
        }

        [XmlAttribute("ShelfLifeDays")] public int ShelfLifeDays { get; set; }
        [XmlAttribute("Price")] public decimal Price { get; set; }
        [XmlAttribute("QuantityInStock")] public int QuantityInStock { get; set; }

        [XmlIgnore] public DateTime? ExpirationDate { get; set; }
        [XmlAttribute("ExpirationDate")]
        public string ExpirationDateXml
        {
            get => ExpirationDate.HasValue ? ExpirationDate.Value.ToShortDateString() : "-";
            set => ExpirationDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}