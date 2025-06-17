/*
 TrackPointExtensionsDto.cs

 Назначение:
 DTO-модель (Data Transfer Object) для представления дополнительных (расширенных) данных одной точки трека.
 В рамках формата GPX такие данные обычно находятся в блоке <extensions> внутри <trkpt>.
 Данный класс отражает элемент <speed>, если он присутствует.

 Пример XML:
 <trkpt lat="..." lon="...">
   ...
   <extensions>
     <speed>5.5</speed>
   </extensions>
 </trkpt>
*/

using System;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    [Serializable] // Указывает, что объект может быть сериализован
    public class TrackPointExtensionsDto
    {
        [XmlElement("speed")] // Привязка к XML-элементу <speed> внутри <extensions>
        public double? Speed { get; set; } // Скорость в м/с, если указана. Может отсутствовать, поэтому nullable
    }
}
