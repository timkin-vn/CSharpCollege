using System;

namespace CardFile.Business.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }
    }
}
