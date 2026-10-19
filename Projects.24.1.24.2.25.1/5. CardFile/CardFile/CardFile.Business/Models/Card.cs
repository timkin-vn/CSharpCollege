
using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public decimal Price { get; set; }
        public string FuelType { get; set; }
        public decimal EngineVolume { get; set; }
        public string TransmissionType { get; set; }
        public bool HasAirConditioning { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public string Vin { get; set; }

        public string CarName => $"{Brand} {Model}".Trim();
    }
}
