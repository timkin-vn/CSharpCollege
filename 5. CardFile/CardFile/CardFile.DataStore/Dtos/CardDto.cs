using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime? BorrowedUntil { get; set; }
        public decimal Price { get; set; }

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
