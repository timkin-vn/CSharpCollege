using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    namespace CardFile.DataAccess.FileDataAccess.Entites
    {
        [Serializable]
        public class XmlCard
        {
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
            public string BirthDateXml
            {
                get => BirthDate.ToShortDateString();
                set => BirthDate = DateTime.Parse(value);
            }

            /// <summary>
            /// Дата регистрации в библиотеке
            /// </summary>
            [XmlIgnore]
            public DateTime RegistrationDate { get; set; }

            [XmlAttribute("RegistrationDate")]
            public string RegistrationDateXml
            {
                get => RegistrationDate.ToShortDateString();
                set => RegistrationDate = DateTime.Parse(value);
            }

            /// <summary>
            /// Автор произведения
            /// </summary>
            [XmlAttribute("Autor")]
            public string Autor { get; set; }

            /// <summary>
            /// Жанр произведения
            /// </summary>
            [XmlAttribute("Genre")]
            public string Genre { get; set; }

            /// <summary>
            /// Произведение
            /// </summary>
            [XmlAttribute("Book")]
            public string Book { get; set; }

            /// <summary>
            /// Дата взятия
            /// </summary>
            [XmlIgnore]
            public DateTime GetDate { get; set; }

            [XmlAttribute("GetDate")]
            public string GetDateXml
            {
                get => GetDate.ToShortDateString();
                set => GetDate = DateTime.Parse(value);
            }

            /// <summary>
            /// Дата возврата
            /// </summary>
            [XmlIgnore]
            public DateTime? RefundDate { get; set; }

            [XmlAttribute("RefundDate")]
            public string RefundDateXml
            {
                get => RefundDate.HasValue ? RefundDate.Value.ToShortDateString() : "-";
                set => RefundDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
            }

            /// <summary>
            /// Всего взято книг (не за раз, а за все время)
            /// </summary>
            [XmlAttribute("Collection")]
            public decimal Collection { get; set; }
        }
    }
    [Serializable]
    public class XmlCard
    {
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
        public string BirthDateXml
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        [XmlIgnore]
        /// <summary>
        /// Дата регистрации в библиотеке
        /// </summary>
        public DateTime RegistrationDate { get; set; }

        [XmlAttribute("RegistrationDate")]
        public string RegistrationDateXml
        {
            get => RegistrationDate.ToShortDateString();
            set => RegistrationDate = DateTime.Parse(value);
        }

        [XmlAttribute("Autor")]
        /// <summary>
        /// Автор произведения
        /// </summary>
        public string Autor { get; set; }

        [XmlAttribute("Genre")]
        /// <summary>
        /// Жанр произведения
        /// </summary>
        public string Genre { get; set; }

        [XmlAttribute("Book")]
        /// <summary>
        /// Произведение
        /// </summary>
        public string Book { get; set; }

        /// <summary>
        /// Дата взятия
        /// </summary>
        [XmlIgnore]
        public DateTime GetDate {  get; set; }

        [XmlAttribute("GetDate")]
        public string GetDateXml
        {
            get => GetDate.ToShortDateString();
            set => GetDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата возврата
        /// </summary>
        [XmlIgnore]
        public DateTime? RefundDate { get; set; }

        [XmlAttribute("RefundDate")]
        public string RefundDateXml
        {
            get => RefundDate.HasValue ? RefundDate.Value.ToShortDateString() : "-";
            set => RefundDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Всего взято книг (не за раз, а за все время)
        /// </summary>
        [XmlAttribute("Collection")]
        public decimal Collection { get; set; }
    }
}
