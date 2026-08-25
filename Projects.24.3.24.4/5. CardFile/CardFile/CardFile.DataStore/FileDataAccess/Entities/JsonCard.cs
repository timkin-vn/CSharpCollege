using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [JsonIgnore]
        public DateTime BirthDate { get; set; }

        [JsonProperty("BirthDate")]
        public string BirthDateText
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Подразделение
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Дата трудоустройства
        /// </summary>
        [JsonIgnore]
        public DateTime EmploymentDate { get; set; }

        [JsonProperty("EmploymentDate")]
        public string EmploymentDateText
        {
            get => EmploymentDate.ToShortDateString();
            set => EmploymentDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        [JsonIgnore]
        public DateTime? DismissalDate { get; set; }

        [JsonProperty("DismissalDate")]
        public string DismissalDateText
        {
            get => DismissalDate?.ToShortDateString() ?? "-";
            set => DismissalDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }
    }
}
