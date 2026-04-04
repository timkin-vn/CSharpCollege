using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }

        public string CauseOfDeath { get; set; }

        public string PlaceOfDeath { get; set; }

        public DateTime AdmissionDate { get; set; }

        public string SectionNumber { get; set; }

        public bool IsReleased { get; set; }
    }
}