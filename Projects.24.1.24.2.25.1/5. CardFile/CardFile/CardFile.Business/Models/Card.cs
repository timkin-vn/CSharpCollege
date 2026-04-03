using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    /// Бизнес-модель карточки товара
    public class Card
    {
        public int Id { get; set; }

        // Составное поле хращееся по частям
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }

        public DateTime ManufactureDate { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? WriteOffDate { get; set; }   // Поле что может отсуствовать
        public decimal Price { get; set; }
    }
}