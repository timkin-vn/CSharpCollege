using Newtonsoft.Json;
using System;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public DateTime BirthDate { get; set; }

        [JsonProperty("BirthDate")]
        public string BirthDateText
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        [JsonIgnore]
        public DateTime DeathDate { get; set; }

        [JsonProperty("DeathDate")]
        public string DeathDateText
        {
            get => DeathDate.ToShortDateString();
            set => DeathDate = DateTime.Parse(value);
        }

        public string CauseOfDeath { get; set; }

        public string PlaceOfDeath { get; set; }

        [JsonIgnore]
        public DateTime AdmissionDate { get; set; }

        [JsonProperty("AdmissionDate")]
        public string AdmissionDateText
        {
            get => AdmissionDate.ToShortDateString();
            set => AdmissionDate = DateTime.Parse(value);
        }

        public string SectionNumber { get; set; }

        public bool IsReleased { get; set; }
    }
}