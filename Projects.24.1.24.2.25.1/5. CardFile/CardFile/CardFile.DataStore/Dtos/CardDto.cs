using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardFile.DataStore.Dtos
{
    /// DTO для хранения данных карточки товара.
    public class CardDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductColor { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? WriteOffDate { get; set; }
        public decimal Price { get; set; }

        public CardDto Clone() => Mapping.Mapper.Map<CardDto>(this);
    }
}