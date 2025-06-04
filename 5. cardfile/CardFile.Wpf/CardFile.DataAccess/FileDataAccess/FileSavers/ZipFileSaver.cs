using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.Entites;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class ZipFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                using (var es = entry.Open())
                using (var reader = new StreamReader(es))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);

                    var mappedCards = Mapping.Mapper.Map<List<CardDto>>(xmlCollection.Cards);
                    collection.ReplaceAll(mappedCards);
                    collection.CurrentId = xmlCollection.CurrentId;
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            var xmlCollection = new XmlCardCollection
            {
                CurrentId = collection.CurrentId,
                Cards = Mapping.Mapper.Map<List<XmlCard>>(collection.GetAll().ToList())
            };

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                using (var es = entry.Open())
                using (var writer = new StreamWriter(es))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    serializer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}