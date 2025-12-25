using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Models
{
    public class Card
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
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Дата регистрации в библиотеке
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        /// <summary>
        /// Автор произведения
        /// </summary>

        public string Autor { get; set; }

        /// <summary>
        /// Жанр произведения
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Произведение
        /// </summary>
        public string Book { get; set; }

        /// <summary>
        /// Дата взятия
        /// </summary>
        public DateTime GetDate { get; set; }

        /// <summary>
        /// Дата возврата
        /// </summary>
        public DateTime? RefundDate { get; set; }

        /// <summary>
        /// Всего взято книг (не за раз, а за все время)
        /// </summary>
        public decimal Collection { get; set; }

    }
}
