using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        public string CourseNumber { get; set; }

        public string Tutor { get; set; }

        public string SpecialProgram { get; set; }

        public int NumberStudents { get; set; }
    }
}
