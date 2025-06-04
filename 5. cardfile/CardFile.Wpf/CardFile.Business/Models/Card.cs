using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ShelfLifeDays { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}