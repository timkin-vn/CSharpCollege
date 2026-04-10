using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
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

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
