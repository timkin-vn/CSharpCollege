using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class JsonCard
    {
        public int Id { get; set; }

        public string CourseNumber { get; set; }

        public string Tutor { get; set; }

        public string BadSpecialProgramClass { get; set; }

        public int NumberStudents { get; set; }
    }
}
