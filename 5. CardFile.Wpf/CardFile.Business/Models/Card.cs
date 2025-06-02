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

        public string Brand { get; set; }          // Марка авто
        public string Model { get; set; }          // Модель авто
        public int Year { get; set; }              // Год выпуска
        public string Vin { get; set; }            // VIN-номер
        public string Color { get; set; }          // Цвет авто
        public decimal Price { get; set; }         // Цена
        public DateTime ArrivalDate { get; set; }  // Дата поступления в салон
        public DateTime? SaleDate { get; set; }    // Дата продажи (nullable)
    }
}
