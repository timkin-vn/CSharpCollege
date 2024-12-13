using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }


        public string Title { get; set; }

        public int Count_actor { get; set; }

        public DateTime DateRelease { get; set; }
        public string Director { get; set; }

        public string FilmReuge { get; set; }


        public decimal Price { get; set; }
    }
}
