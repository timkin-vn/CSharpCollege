using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Platform { get; set; }
        public string Publisher { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public decimal Price { get; set; }
    }
}
