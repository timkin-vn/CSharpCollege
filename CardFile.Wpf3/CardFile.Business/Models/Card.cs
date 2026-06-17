using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
    {
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
    }
}
