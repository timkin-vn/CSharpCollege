/*
 DataService.cs

 Назначение:
 Сервис доступа к данным (GPX-файлу).
 Отвечает за десериализацию GPX-файла в объектную модель `GpxData`.
 Использует вспомогательный XML-чтение `IgnoreNamespaceXmlTextReader` для упрощения обработки пространств имён XML.

 Пример использования:
 var dataService = new DataService();
 var gpxData = dataService.ReadFromFile("path/to/file.gpx");
*/

using GpxDataShow.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Services
{
    public class DataService
    {
        /// <summary>
        /// Загружает GPX-файл и преобразует его содержимое в объект модели GpxData.
        /// </summary>
        /// <param name="fileName">Полный путь к GPX-файлу</param>
        /// <returns>Объект GpxData, содержащий данные трека</returns>
        public GpxData ReadFromFile(string fileName)
        {
            // Открытие файла для чтения
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (TextReader textReader = new StreamReader(fs))
                {
                    // Используем специальный XmlTextReader, игнорирующий пространства имён
                    using (XmlTextReader reader = new IgnoreNamespaceXmlTextReader(textReader))
                    {
                        var serializer = new XmlSerializer(typeof(GpxData));
                        // Десериализация XML → GpxData
                        return (GpxData)serializer.Deserialize(reader);
                    }
                }
            }
        }
    }
}
