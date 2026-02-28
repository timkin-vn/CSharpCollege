using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Vin { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("ArrivalDate")]
        public string ArrivalDateXml
        {
            get => ArrivalDate.ToShortDateString();
            set => ArrivalDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime? SaleDate { get; set; }

        [JsonProperty("SaleDate")]
        public string SaleDateXml
        {
            get => SaleDate.HasValue ? SaleDate.Value.ToShortDateString() : "-";
            set => SaleDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }
    }
}