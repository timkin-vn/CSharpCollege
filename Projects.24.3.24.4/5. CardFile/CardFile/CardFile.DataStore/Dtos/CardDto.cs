using CardFile.Common.Infrastructure;
using System;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? DismissalDate { get; set; }
        public decimal Salary { get; set; }

        internal CardDto Clone()
        {
            return Mapping.Mapper.Map<CardDto>(this);
        }
    }
}
