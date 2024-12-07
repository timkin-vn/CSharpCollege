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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [JsonPropertyName("BirthDate")]
        public long BirthDateTicks
        {
            get => BirthDate.Ticks;
            set => BirthDate = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime BirthDate { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public int SubordinatesCount { get; set; }

        public decimal PaymentAmount { get; set; }
    }
}
