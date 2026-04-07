using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class ZipFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                try
                {
                    var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open())
                    using (var reader = new StreamReader(es))
                    {
                        var serializer = new XmlSerializer(typeof(XmlLetterCollection));
                        var xmlCollection = (XmlLetterCollection)serializer.Deserialize(reader);
                        collection.ReplaceAll(Mapping.Mapper.Map<List<LetterDto>>(xmlCollection.Letters), xmlCollection.NextId);
                    }
                }
                catch { throw new Exception("Неверный формат файла"); }
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
            using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                using (var es = entry.Open())
                using (var writer = new StreamWriter(es))
                {
                    var serializer = new XmlSerializer(typeof(XmlLetterCollection));
                    serializer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}