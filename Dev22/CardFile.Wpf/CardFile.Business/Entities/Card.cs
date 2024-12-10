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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public int SubordinatesCount { get; set; }

        public decimal PaymentAmount { get; set; }
    }
}
