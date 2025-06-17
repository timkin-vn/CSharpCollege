/*
 TrackDto.cs

 Назначение:
 Представляет собой DTO-модель (Data Transfer Object) для сериализации/десериализации трека из GPX-файла.
 Используется для работы с XML-структурой GPX, где <trk> — это трек, содержащий название и один или несколько сегментов (<trkseg>).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable] // Атрибут указывает, что объект можно сериализовать
    public class TrackDto
    {
        [XmlElement("name")] // Привязка к XML-элементу <name>
        public string Name { get; set; } // Название трека (может быть null или пустым)

        [XmlElement("trkseg")] // Привязка к XML-элементам <trkseg>
        public List<TrackSegmentDto> Segments { get; set; } // Список сегментов трека, каждый содержит точки пути
    }
}