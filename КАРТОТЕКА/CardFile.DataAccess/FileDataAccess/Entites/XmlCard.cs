using System;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    [Serializable]
    public class XmlCard
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// Номер машины
        /// </summary>
        [XmlAttribute("LicensePlate")]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Владелец
        /// </summary>
        [XmlAttribute("OwnerName")]
        public string OwnerName { get; set; }

        /// <summary>
        /// Тип транспорта
        /// </summary>
        [XmlAttribute("VehicleType")]
        public string VehicleType { get; set; }

        /// <summary>
        /// Дата въезда
        /// </summary>
        [XmlIgnore]
        public DateTime EntryDate { get; set; }

        [XmlAttribute("EntryDate")]
        public string EntryDateXml
        {
            get => EntryDate.ToShortDateString();
            set => EntryDate = DateTime.Parse(value);
        }

        /// <summary>
        /// Дата выезда
        /// </summary>
        [XmlIgnore]
        public DateTime? ExitDate { get; set; }

        [XmlAttribute("ExitDate")]
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
        [XmlAttribute("HourlyRate")]
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        [XmlAttribute("IsPaid")]
        public bool IsPaid { get; set; }

        /// <summary>
        /// Парковочное место
        /// </summary>
        [XmlAttribute("ParkingSpot")]
        public int ParkingSpot { get; set; }
    }
}