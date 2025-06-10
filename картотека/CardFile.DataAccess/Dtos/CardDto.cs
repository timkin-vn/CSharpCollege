using System;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
        public int Id { get; set; }

        /// <summary>
        /// Номер ремонтного бокса
        /// </summary>
        public int BayNumber { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Тип услуги (например, шиномонтаж, диагностика)
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// Дата приёма автомобиля
        /// </summary>
        public DateTime DropOffDate { get; set; }

        /// <summary>
        /// Дата завершения работ / выдачи авто
        /// </summary>
        public DateTime? PickupDate { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal ServiceCost { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Имя механика
        /// </summary>
        public string MechanicName { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                BayNumber = BayNumber,
                ClientName = ClientName,
                ServiceType = ServiceType,
                DropOffDate = DropOffDate,
                PickupDate = PickupDate,
                ServiceCost = ServiceCost,
                IsPaid = IsPaid,
                MechanicName = MechanicName
            };
        }
    }
}
