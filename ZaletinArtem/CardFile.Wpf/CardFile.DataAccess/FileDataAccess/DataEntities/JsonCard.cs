using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.DataEntities
{
    [Serializable]
    internal class JsonCard
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        [JsonPropertyName("PublicationDate")]
        public long PublicationDateTicks
        {
            get => PublicationDate.Ticks;
            set => PublicationDate = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime PublicationDate { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }

        public decimal Price { get; set; }
    }
}
