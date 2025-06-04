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

        public string Name { get; set; }

        public DateTime DeliverDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int Count { get; set; }

        public double Rating { get; set; }
    }
}
