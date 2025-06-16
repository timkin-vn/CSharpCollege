/*
 Этот файл — модель данных для десериализации GPX-файла, 
который представляет собой XML-документ с GPS-треками. 
Класс GpxData служит корневой моделью для чтения GPX-файлов в вашем приложении.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Models
{
    //позволяет сериализовать объект (например, сохранить в файл).
    [Serializable]
    //указывает, что корневой элемент XML называется <gpx>.
    [XmlRoot("gpx")]
    public class GpxData
    {
        //[XmlAttribute("version")] и другие — связывают свойства C# с атрибутами XML.
        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlAttribute("creator")]
        public string Creator { get; set; }
        // указывает, что свойство Track соответствует XML-элементу <trk>.
        [XmlElement("trk")]
        public TrackDto Track { get; set; }
    }
}
