using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string DishName { get; set; }

        public string Category { get; set; }
        public string Description { get; set; }

        public int PortionSize { get; set; }
        public double Price { get; set; }
        public bool IsAvaliableNow { get; set; }
        public bool SeasonDish { get; set; }
        public bool IsVegan { get; set; }
        public bool IsSpicy { get; set; }
    }
}