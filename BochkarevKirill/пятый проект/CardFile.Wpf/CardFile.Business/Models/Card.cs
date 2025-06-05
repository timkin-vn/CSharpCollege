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
        /// Имя клиента
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество клиента
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата привоза автомобиля
        /// </summary>
        public DateTime ArrivalDate { get; set; }

        /// <summary>
        /// Причина ремонта
        /// </summary>
        public string RepairReason { get; set; }

        /// <summary>
        /// Дата завершения ремонта
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Стоимость ремонта
        /// </summary>
        public decimal RepairCost { get; set; }
    }
}