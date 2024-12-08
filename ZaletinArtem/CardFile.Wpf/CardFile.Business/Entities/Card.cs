using System;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; } 
        public string Genre { get; set; } 
        public int PageCount { get; set; } 
        public decimal Price { get; set; }
    }
}
