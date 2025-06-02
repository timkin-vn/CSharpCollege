using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object

        /// <summary>
        /// Id
        /// </summary>
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
        public DateTime BirthDate { get; set; }

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
        public DateTime EmploymentDate { get; set; }

        /// <summary>
        /// Дата увольнения
        /// </summary>
        public DateTime? DismissalDate { get; set; }

        /// <summary>
        /// Оклад
        /// </summary>
        public decimal Salary { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                BirthDate = BirthDate,
                Department = Department,
                Position = Position,
                EmploymentDate = EmploymentDate,
                DismissalDate = DismissalDate,
                Salary = Salary,
            };
        }
    }
}
