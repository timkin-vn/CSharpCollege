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

        public string BookName { get; set; }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public string Genre { get; set; }

        [JsonIgnore]
        public DateTime DateOfPublishing { get; set; }

        [JsonProperty("DateOfPublishing")]
        public string DateOfPublishingXml
        {
            get => DateOfPublishing.ToShortDateString();
            set => DateOfPublishing = DateTime.Parse(value);
        }

        public string Edition { get; set; }

        public int Price { get; set; }
        public int AmountOfPages { get; set; }

        [JsonIgnore]
        public DateTime? DateOfDelisting { get; set; }

        [JsonProperty("DateOfDelisting")]
        public string DateOfDelistingXml
        {
            get => DateOfDelisting.HasValue ? DateOfDelisting.Value.ToShortDateString() : "-";
            set => DateOfDelisting = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        public decimal Rating { get; set; }
    }
}
