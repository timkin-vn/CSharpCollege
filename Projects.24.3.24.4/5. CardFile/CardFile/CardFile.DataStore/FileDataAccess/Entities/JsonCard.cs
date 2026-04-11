using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ProductName { get; set; }

        [JsonIgnore]
        public DateTime OrderDate { get; set; }

        [JsonProperty("OrderDate")]
        public string OrderDateText
        {
            get => OrderDate.ToShortDateString();
            set => OrderDate = DateTime.Parse(value);
        }

        public string Address { get; set; }
        public string DeliveryMethod { get; set; }

        [JsonIgnore]
        public DateTime ShippingDate { get; set; }

        [JsonProperty("ShippingDate")]
        public string ShippingDateText
        {
            get => ShippingDate.ToShortDateString();
            set => ShippingDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? ReceivedDate { get; set; }

        [JsonProperty("ReceivedDate")]
        public string ReceivedDateText
        {
            get => ReceivedDate?.ToShortDateString() ?? "-";
            set => ReceivedDate = value == "-" ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal TotalAmount { get; set; }
    }
}
