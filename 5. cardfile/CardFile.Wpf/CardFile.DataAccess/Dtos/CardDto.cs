using System;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
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

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                ProductName = ProductName,
                Category = Category,
                Manufacturer = Manufacturer,
                ProductionDate = ProductionDate,
                ShelfLifeDays = ShelfLifeDays,
                Price = Price,
                QuantityInStock = QuantityInStock,
                ExpirationDate = ExpirationDate
            };
        }
    }
}