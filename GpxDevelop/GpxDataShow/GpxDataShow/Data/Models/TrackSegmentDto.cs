/*
 TrackSegmentDto.cs

 Назначение:
 DTO-модель (Data Transfer Object) для представления сегмента трека из GPX-файла.
 Соответствует XML-элементу <trkseg>, который содержит список точек <trkpt>.

 Сегмент используется для группировки связанных точек маршрута (например, между паузами записи трека).
 Один трек (<trk>) может содержать несколько сегментов (<trkseg>).

 Пример XML:
 <trk>
   <trkseg>
     <trkpt lat="..." lon="...">...</trkpt>
     <trkpt lat="..." lon="...">...</trkpt>
   </trkseg>
 </trk>
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable] // Разрешает сериализацию этого объекта
    public class TrackSegmentDto
    {
        [XmlElement("trkpt")] // Привязка к элементам <trkpt> внутри <trkseg>
        public List<TrackPointDto> Points { get; set; } // Список точек сегмента
    }
}

