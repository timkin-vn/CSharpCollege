using System;

namespace CardFile.DataAccess.Dtos
{
    public class CardDto
    {
        // DTO = Data Transfer Object
        public int Id { get; set; }

        /// <summary>
        /// Номер машины
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Владелец
        /// </summary>
        public string OwnerName { get; set; }

        /// <summary>
        /// Тип транспорта
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Дата въезда
        /// </summary>
        public DateTime EntryDate { get; set; }

        /// <summary>
        /// Дата выезда
        /// </summary>
        public DateTime? ExitDate { get; set; }

        /// <summary>
        /// Стоимость в час
        /// </summary>
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// Парковочное место
        /// </summary>
        public int ParkingSpot { get; set; }

        public CardDto Clone()
        {
            return new CardDto
            {
                Id = Id,
                LicensePlate = LicensePlate,
                OwnerName = OwnerName,
                VehicleType = VehicleType,
                EntryDate = EntryDate,
                ExitDate = ExitDate,
                HourlyRate = HourlyRate,
                IsPaid = IsPaid,
                ParkingSpot = ParkingSpot
            };
        }
    }
}