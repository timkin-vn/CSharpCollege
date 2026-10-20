using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Color { get; set; }
        public string Owner { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? DeregistrationDate { get; set; }
        public decimal Price { get; set; }

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
