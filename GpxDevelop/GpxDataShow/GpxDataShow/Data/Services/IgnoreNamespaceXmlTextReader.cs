/*
 IgnoreNamespaceXmlTextReader.cs

 Назначение:
 Специализированный XmlTextReader, который принудительно игнорирует пространства имён в XML-документе.

 Это нужно, когда XML-документ (например, GPX) содержит пространства имён (xmlns), а ваш класс десериализации не настроен на их обработку.
 Переопределяя свойство NamespaceURI, можно отключить проверку пространств имён при десериализации.

 Используется в DataService.cs при чтении GPX-файла.

 Пример:
 <gpx xmlns="http://www.topografix.com/GPX/1/1">...</gpx>
 → будет восприниматься как <gpx> без пространства имён.
*/

using System.IO;
using System.Xml;

namespace GpxDataShow.Data.Services
{
    /// <summary>
    /// XmlTextReader, игнорирующий пространства имён при разборе XML.
    /// </summary>
    internal class IgnoreNamespaceXmlTextReader : XmlTextReader
    {
        /// <summary>
        /// Конструктор принимает стандартный TextReader.
        /// </summary>
        public IgnoreNamespaceXmlTextReader(TextReader reader) : base(reader)
        {
        }

        /// <summary>
        /// Всегда возвращает пустую строку как пространство имён.
        /// Это отключает обработку пространств имён в XmlSerializer.
        /// </summary>
        public override string NamespaceURI => "";
    }
}
