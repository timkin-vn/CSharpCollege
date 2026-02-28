using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class XmlFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);

                    collection.ReplaceAll(Mapping.Mapper.Map<List<CardDto>>(xmlCollection.Cards), xmlCollection.CurrentId);
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            var xmlCollection = new XmlCardCollection { CurrentId = collection.CurrentId };
            xmlCollection.Cards.AddRange(Mapping.Mapper.Map<List<XmlCard>>(collection.GetAll()));

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    serializer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}