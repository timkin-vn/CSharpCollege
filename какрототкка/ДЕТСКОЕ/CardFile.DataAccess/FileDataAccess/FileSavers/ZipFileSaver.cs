using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
                                var serializer = new XmlSerializer(typeof(List<CardDto>));
                                var cards = (List<CardDto>)serializer.Deserialize(reader);
                                collection.ReplaceAll(cards);
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
            var cards = new List<CardDto>(collection.GetAll());

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var es = entry.Open())
                    {
                        using (var writer = new StreamWriter(es))
                        {
                            var serializer = new XmlSerializer(typeof(List<CardDto>));
                            serializer.Serialize(writer, cards);
                        }
                    }
                }
            }
        }
    }
}