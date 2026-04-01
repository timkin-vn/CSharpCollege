using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public int Copies { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public BookDto Clone()
        {
            return Mapping.Mapper.Map<BookDto>(this);
        }
    }
}
