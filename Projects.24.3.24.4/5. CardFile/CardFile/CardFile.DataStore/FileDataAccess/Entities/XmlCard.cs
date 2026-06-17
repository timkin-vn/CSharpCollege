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
        /// <summary>
        /// Id
        /// </summary>
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [XmlAttribute("MiddleName")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [XmlAttribute("LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [XmlIgnore]
        public DateTime BirthDate { get; set; }

        [XmlAttribute("BirthDate")]
        public string BirthDateText
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Подразделение
        /// </summary>
        [XmlAttribute("Department")]
        public string Department { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [XmlAttribute("Position")]
        public string Position { get; set; }

        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        [XmlIgnore]
        public DateTime EmploymentDate { get; set; }

        [XmlAttribute("EmploymentDate")]
        public string EmploymentDateText
        {
            get => EmploymentDate.ToShortDateString();
            set => EmploymentDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        [XmlIgnore]
        public DateTime? DismissalDate { get; set; }

        [XmlAttribute("DismissalDate")]
        public string DismissalDateText
        {
            get => DismissalDate?.ToShortDateString() ?? "-";
            set => DismissalDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Оклад
        /// </summary>
        [XmlAttribute("Salary")]
        public decimal Salary { get; set; }
    }
}
