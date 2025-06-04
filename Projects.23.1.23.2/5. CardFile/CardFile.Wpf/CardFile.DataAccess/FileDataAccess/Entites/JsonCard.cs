using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public DateTime DeliverDate { get; set; }

        [JsonProperty("DeliverDate")]
        public string DeliverDateXml
        {
            get => DeliverDate.ToShortDateString();
            set => DeliverDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty("EmploymentDate")]
        public string ExpirationDateXml
        {
            get => ExpirationDate.ToShortDateString();
            set => ExpirationDate = DateTime.Parse(value);
        }

        public int Count { get; set; }

        public double Rating { get; set; }
    }
}
