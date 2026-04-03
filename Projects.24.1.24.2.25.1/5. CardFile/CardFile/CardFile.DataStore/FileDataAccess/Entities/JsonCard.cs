using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }

        [JsonIgnore]
        public DateTime ManufactureDate { get; set; }
        [JsonProperty("ManufactureDate")]
        public string ManufactureDateText
        {
            get => ManufactureDate.ToShortDateString();
            set => ManufactureDate = DateTime.Parse(value);
        }

        public string Category { get; set; }
        public string Manufacturer { get; set; }

        [JsonIgnore]
        public DateTime ReceiptDate { get; set; }
        [JsonProperty("ReceiptDate")]
        public string ReceiptDateText
        {
            get => ReceiptDate.ToShortDateString();
            set => ReceiptDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? WriteOffDate { get; set; }
        [JsonProperty("WriteOffDate")]
        public string WriteOffDateText
        {
            get => WriteOffDate?.ToShortDateString() ?? "-";
            set => WriteOffDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Price { get; set; }
    }
}
