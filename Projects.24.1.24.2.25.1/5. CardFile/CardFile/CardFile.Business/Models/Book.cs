using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public int Year { get; set; }

        public int Copies { get; set; }

        public DateTime AddedDate { get; set; }  // Дата добавления

        public DateTime? DeletedDate { get; set; } // Дата удаления
    }
}
