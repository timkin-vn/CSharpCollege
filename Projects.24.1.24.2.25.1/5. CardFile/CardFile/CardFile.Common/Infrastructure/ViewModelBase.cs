using System;

namespace CardFile.DataStore.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Faculty { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public string RecordBookNumber { get; set; }
        public double AverageGrade { get; set; }
        public DateTime AdmissionDate { get; set; }
        public DateTime? DismissalDate { get; set; }
    }
}