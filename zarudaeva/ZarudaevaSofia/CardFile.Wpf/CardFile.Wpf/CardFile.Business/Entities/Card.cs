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

        public string MovieName { get; set; }

        public string MovieType { get; set; }

       

        public DateTime DateReles { get; set; }  
        public TimeSpan TimeGoes { get; set; }

        public decimal PriseOneTickets { get; set; }

        public int CountTickets { get; set; }
        public short LinePlace { get; set; }
        public short Place { get; set; }
    }
}
