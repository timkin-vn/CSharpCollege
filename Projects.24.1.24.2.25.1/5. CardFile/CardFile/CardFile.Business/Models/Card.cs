using System;

namespace CardFile.Business.Models
{
    public class Car
    {
      
        public int Id { get; set; }

      
        public string Brand { get; set; }

      
        public string Model { get; set; }

       
        public string Color { get; set; }

       
        public DateTime ReleaseDate { get; set; }

       
        public string BodyType { get; set; }

        
        public string Configuration { get; set; }

       
        public DateTime ArrivalDate { get; set; }

       
        public DateTime? SaleDate { get; set; }

      
        public decimal Price { get; set; }
    }
}