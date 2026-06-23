using Newtonsoft.Json;
using System;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
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
        [JsonIgnore]
        public DateTime EntryDate { get; set; }

        [JsonProperty("EntryDate")]
        public string EntryDateXml
        {
            get => EntryDate.ToShortDateString();
            set => EntryDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата выезда
        /// </summary>
        [JsonIgnore]
        public DateTime? ExitDate { get; set; }

        [JsonProperty("ExitDate")]
        public string ExitDateXml
        {
            get => ExitDate.HasValue ? ExitDate.Value.ToShortDateString() : "-";
            set
            {
                if (value == "-")
                {
                    ExitDate = null;
                }
                else
                {
                    ExitDate = DateTime.Parse(value);
                }
            }
        }

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
    }
}