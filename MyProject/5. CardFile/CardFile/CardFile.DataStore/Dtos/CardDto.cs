using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
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

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                BirthDate = BirthDate,
                DeathDate = DeathDate,
                CauseOfDeath = CauseOfDeath,
                PlaceOfDeath = PlaceOfDeath,
                AdmissionDate = AdmissionDate,
                SectionNumber = SectionNumber,
                IsReleased = IsReleased
            };
        }
    }
}