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

        public DateTime EXP { get; set; }

        public string Fabricator { get; set; }

        public string Section { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}
