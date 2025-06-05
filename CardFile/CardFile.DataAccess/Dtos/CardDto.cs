using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
        public int Id { get; set; }
        public string DishName { get; set; }

        public string Category {  get; set; }
        public string Description { get; set; }

        public int PortionSize { get; set; }
        public double Price { get; set; }
        public bool IsAvaliableNow { get; set; }
        public bool SeasonDish {  get; set; }
        public bool IsVegan { get; set; }
        public bool IsSpicy { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                DishName = DishName,
                Category = Category,
                Description = Description,
                PortionSize = PortionSize,
                Price = Price,
                IsAvaliableNow = IsAvaliableNow,
                SeasonDish = SeasonDish,
                IsVegan = IsVegan,
                IsSpicy = IsSpicy,
            };
        }
    }    
}
