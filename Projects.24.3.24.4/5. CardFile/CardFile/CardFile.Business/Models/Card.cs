using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string AlbumTitle { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Label { get; set; }
        public string Format { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? LastListenDate { get; set; }
        public decimal Price { get; set; }
    }
}
