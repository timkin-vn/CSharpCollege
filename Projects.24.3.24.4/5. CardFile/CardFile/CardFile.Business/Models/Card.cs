using System;

namespace CardFile.Business.Models 
{
    public class Card
    {
        public int Id { get; set; }
        public string Brand { get; set; }        
        public string ModelName { get; set; }     
        public int Year { get; set; }             
        public string VinCode { get; set; }       
        public decimal Price { get; set; }       
        public string EngineType { get; set; }    
        public double EngineVolume { get; set; }   
        public int Mileage { get; set; }           
        public string Color { get; set; }          
        public DateTime? LastServiceDate { get; set; } 
    }
}