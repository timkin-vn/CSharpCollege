using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class XmlFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                var serializer = new XmlSerializer(typeof(XmlLetterCollection));
                var xmlCollection = (XmlLetterCollection)serializer.Deserialize(reader);
                collection.ReplaceAll(Mapping.Mapper.Map<List<LetterDto>>(xmlCollection.Letters), xmlCollection.NextId);
            }
        }

        public void SaveToFile(string fileName, LetterCollection collection)
        {
            var xmlCollection = new XmlLetterCollection
            {
                NextId = collection.NextId,
                Letters = Mapping.Mapper.Map<List<XmlLetter>>(collection.GetAll()),
            };
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                var serializer = new XmlSerializer(typeof(XmlLetterCollection));
                serializer.Serialize(writer, xmlCollection);
            }
        }
    }
}