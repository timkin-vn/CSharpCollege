using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return Mapping.Mapper.Map<CardDto>(this);
        }

        public void Update(CardDto from)
        {
            Mapping.Mapper.Map(from, this);

        }
    }
}
