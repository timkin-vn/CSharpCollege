using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
        public int Id { get; set; }
        public string BookName { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Genre { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public string Edition { get; set; }
        public int Price { get; set; }
        public int AmountOfPages { get; set; }
        public DateTime? DateOfDelisting { get; set; }
        public decimal Rating { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                BookName = BookName,
                AuthorFirstName = AuthorFirstName,
                AuthorLastName = AuthorLastName,
                Genre = Genre,
                DateOfPublishing = DateOfPublishing,
                Edition = Edition,
                Price = Price,
                AmountOfPages = AmountOfPages,
                DateOfDelisting = DateOfDelisting,
                Rating = Rating,
            };
        }
    }
}
