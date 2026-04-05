using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class XmlCard
    {
        [XmlAttribute("Id")]
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        [XmlAttribute("FirstName")]
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        [XmlAttribute("MiddleName")]
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        [XmlAttribute("LastName")]
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        [XmlAttribute("BirthDate")]
        public string BirthDateText
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        [XmlAttribute("Department")]
        /// <summary>
        /// Подразделение
        /// </summary>
        public string Department { get; set; }

        [XmlAttribute("Position")]
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        [XmlIgnore]
        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        public DateTime EmploymentDate { get; set; }

        [XmlAttribute("EmploymentDate")]
        public string EmploymentDateText
        {
            get => EmploymentDate.ToShortDateString();
            set => EmploymentDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; set; }

        [XmlAttribute("DismissalDate")]
        public string DismissalDateText
        {
            get => DismissalDate?.ToShortDateString() ?? "-";
            set => DismissalDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        [XmlAttribute("Salary")]
        /// <summary>
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }
    }
}
