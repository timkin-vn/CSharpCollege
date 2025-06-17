/*
 TrackPointDto.cs

 Назначение:
 DTO-модель (Data Transfer Object) для представления одной точки трека из GPX-файла.
 Отражает структуру <trkpt> в GPX: содержит координаты, высоту, время, а также дополнительную информацию (точность, количество спутников и пр.).
 Используется для сериализации и десериализации XML-данных.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable] // Указывает, что класс может быть сериализован
    public class TrackPointDto
    {
        // Атрибуты координат (обязательные поля в GPX)
        [XmlAttribute("lat")]
        public string Latitude { get; set; } // Широта (в строковом формате, далее парсится в double)

        [XmlAttribute("lon")]
        public string Longitude { get; set; } // Долгота

        // Элементы внутри <trkpt>
        [XmlElement("ele")]
        public double? Elevation { get; set; } // Высота над уровнем моря (в метрах), может отсутствовать

        [XmlElement("time")]
        public string Time { get; set; } // Время фиксации точки (в формате ISO 8601), далее парсится в DateTime

        // Дополнительная информация о точке
        [XmlElement("hdop")]
        public double? HorizontalDispersion { get; set; } // Горизонтальная точность (HDOP) — оценка точности GPS

        [XmlElement("fix")]
        public string Fix { get; set; } // Тип фиксации сигнала GPS (например, 2d, 3d)

        [XmlElement("sat")]
        public int? SatelliteCount { get; set; } // Количество спутников, использованных для получения позиции

        [XmlElement("extensions")]
        public TrackPointExtensionsDto Extensions { get; set; } // Дополнительные данные, если присутствуют

        [XmlElement("cmt")]
        public string Comment { get; set; } // Комментарий к точке (необязательное поле)
    }
}
