using System;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("ClientFirstName")]
        public string ClientFirstName { get; set; }

        [XmlAttribute("ClientLastName")]
        public string ClientLastName { get; set; }

        [XmlAttribute("ProductName")]
        public string ProductName { get; set; }

        [XmlIgnore]
        public DateTime OrderDate { get; set; }

        [XmlAttribute("OrderDate")]
        public string OrderDateText
        {
            get => OrderDate.ToShortDateString();
            set => OrderDate = DateTime.Parse(value);
        }

        [XmlAttribute("Address")]
        public string Address { get; set; }

        [XmlAttribute("DeliveryMethod")]
        public string DeliveryMethod { get; set; }

        [XmlIgnore]
        public DateTime ShippingDate { get; set; }

        [XmlAttribute("ShippingDate")]
        public string ShippingDateText
        {
            get => ShippingDate.ToShortDateString();
            set => ShippingDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        public DateTime? ReceivedDate { get; set; }

        [XmlAttribute("ReceivedDate")]
        public string ReceivedDateText
        {
            get => ReceivedDate?.ToShortDateString() ?? "-";
            set => ReceivedDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("TotalAmount")]
        public decimal TotalAmount { get; set; }
    }
}
