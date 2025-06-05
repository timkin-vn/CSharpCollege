using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class XmlFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var serializer = new XmlSerializer(typeof(List<CardDto>));
                    var cards = (List<CardDto>)serializer.Deserialize(reader);
                    collection.ReplaceAll(cards);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var cards = new List<CardDto>(collection.GetAll());

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new XmlSerializer(typeof(List<CardDto>));
                    serializer.Serialize(writer, cards);
                }
            }
        }
    }
}