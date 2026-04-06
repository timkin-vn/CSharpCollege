using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LimerList.DataStore.FileAccess.Entities
{
    public class JsonLimer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public DateTime BirthDate { get; set; }
        [JsonProperty("BirthDate")]
        public string BirthDateString
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }
        public string Group { get; set; }
    }
}
