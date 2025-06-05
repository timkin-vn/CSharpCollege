using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
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
        public string BirthDateXml
        {
            get => BirthDate.ToShortDateString();
            set => BirthDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата регистрации в библиотеке
        /// </summary>
        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }

        [JsonProperty("RegistrationDate")]
        public string RegistrationDateXml
        {
            get => RegistrationDate.ToShortDateString();
            set => RegistrationDate = DateTime.Parse(value);
        }

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
        [JsonIgnore]
        public DateTime GetDate { get; set; }

        [JsonProperty("GetDate")]
        public string GetDateXml
        {
            get => GetDate.ToShortDateString();
            set => GetDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата возврата
        /// </summary>
        [JsonIgnore]
        public DateTime? RefundDate { get; set; }

        [JsonProperty("RefundDate")]
        public string RefundDateXml
        {
            get => RefundDate.HasValue ? RefundDate.Value.ToShortDateString() : "-";
            set => RefundDate = (value == "-") ? (DateTime?)null : DateTime.Parse(value);
        }

        /// <summary>
        /// Всего взято книг (не за раз, а за все время)
        /// </summary>
        public decimal Collection { get; set; }
    }
}