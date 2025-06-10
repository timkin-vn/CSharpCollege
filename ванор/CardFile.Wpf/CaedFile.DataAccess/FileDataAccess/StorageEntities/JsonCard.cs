using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class JsonCard
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [JsonPropertyName("BirthDate")]
        public long BirthDateTicks
        {
            get => BirthDate.Ticks;
            set => BirthDate = new DateTime(value);
        }

        [JsonIgnore]
        public DateTime BirthDate { get; set; }

        public decimal PaymentAmount { get; set; }

        public int Circulation { get; set; }

        public string Booktitle { get; set; }

        public string Publisher { get; set; }

        public string Year { get; set; }
    }
}
