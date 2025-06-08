using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.DataEntites;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class ZipFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    try
                    {
                        var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                        using (var es = entry.Open())
                        {
                            using (var reader = new StreamReader(es))
                            {
                                var serializer = new XmlSerializer(typeof(XmlCardCollection));
                                var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);

                                collection.ReplaceAll(xmlCollection.Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)), xmlCollection.NextId);
                            }
                        }
                    }
                    catch
                    {
                        throw new Exception("Неверный формат файла");
                    }
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open())
                    {
                        var xmlCollection = new XmlCardCollection { NextId = collection.NextId, };
                        xmlCollection.Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<XmlCard>(c)));

                        using (var writer = new StreamWriter(es))
                        {
                            var serializer = new XmlSerializer(typeof(XmlCardCollection));
                            serializer.Serialize(writer, xmlCollection);
                        }
                    }
                }
            }
        }
    }
}
