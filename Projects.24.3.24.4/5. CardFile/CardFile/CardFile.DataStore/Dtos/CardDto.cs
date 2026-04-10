using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
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

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
