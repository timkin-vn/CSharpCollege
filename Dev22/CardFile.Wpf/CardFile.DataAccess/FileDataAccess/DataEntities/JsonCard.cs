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

        public int Count_actor { get; set; }

        public DateTime DateRelease { get; set; }
        public long BirthDateTicks
        {
            get => DateRelease.Ticks;
            set => DateRelease = new DateTime(value);
        }
        [JsonIgnore]
        public string Director { get; set; }

        public string FilmReuge { get; set; }


        public decimal Price { get; set; }
      
    }
}
