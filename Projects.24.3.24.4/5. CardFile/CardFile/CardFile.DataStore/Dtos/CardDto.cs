using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.Dtos
{
    public class CardDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Номер группы
        /// </summary>
        public string GroupNumber { get; set; }

        /// <summary>
        /// Номер зачетной книжки
        /// </summary>
        public double RecordBookNumber { get; set; }

        /// <summary>
        /// Средний балл
        /// </summary>
        public double AverageScore { get; set; }

        /// <summary>
        /// Обучение на бюджете
        /// </summary>
        public bool IsBudget { get; set; }

        /// <summary>
        /// Дата отчисления/окончания
        /// </summary>
        public DateTime? ExpulsionDate { get; set; }

        /// <summary>
        /// Путь к файлу фотографии студента
        /// </summary>
        public string PhotoPath { get; set; }
    }
}
