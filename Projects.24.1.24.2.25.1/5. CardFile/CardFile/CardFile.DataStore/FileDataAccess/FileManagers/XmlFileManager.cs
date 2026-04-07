using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class XmlFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);

                    collection.ReplaceAll(
                        Mapping.Mapper.Map<List<CardDto>>(xmlCollection.Cards),
                        xmlCollection.NextId,
                        xmlCollection.Heroes,
                        xmlCollection.Items,
                        xmlCollection.Neutrals);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var xmlCollection = new XmlCardCollection 
            { 
                NextId = collection.NextId,
                Heroes = collection.GetHeroes().ToList(),
                Items = collection.GetItems().ToList(),
                Neutrals = collection.GetNeutrals().ToList(),
                Cards = Mapping.Mapper.Map<List<XmlCard>>(collection.GetAll()),
            };
            //xmlCollection.Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)));

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
