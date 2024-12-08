using System;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Author { get; set; } 
        public DateTime PublicationDate { get; set; } 
        public string Genre { get; set; }
        public int PageCount { get; set; } 
        public decimal Price { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                Title = Title,
                Author = Author,
                PublicationDate = PublicationDate,
                Genre = Genre,
                PageCount = PageCount,
                Price = Price
            };
        }

        public void Update(CardDto from)
        {
            Title = from.Title;
            Author = from.Author;
            PublicationDate = from.PublicationDate;
            Genre = from.Genre;
            PageCount = from.PageCount;
            Price = from.Price;
        }
    }
}
