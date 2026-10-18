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

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Faculty { get; set; }

        public string GroupNumber { get; set; }

        public int RecordBookNumber { get; set; }

        public double AverageScore { get; set; }

        public bool IsBudget { get; set; }

        public DateTime? ExpulsionDate { get; set; }

        public string PhotoPath { get; set; }
    }
}
