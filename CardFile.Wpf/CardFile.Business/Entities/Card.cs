using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public string CourseNumber { get; set; }

        public string Tutor { get; set; }

        public string SpecialProgram { get; set; }
        public int NumberStudents { get; set; }
    }
}
