using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.FileAccess.Entities;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public class ZipFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    try
                    {
                        var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(filePath) + ".xml");
                        using (var es = entry.Open())
                        {
                            using(var reader  = new StreamReader(es))
                            {
                                var serializer = new XmlSerializer(typeof(XmlLimerCollection));
                                var xmlCollection = (XmlLimerCollection)serializer.Deserialize(reader);

                                collection.ReplaceAll(Mapping.Mapper.Map<List<LimerDto>>(xmlCollection.Limers), xmlCollection.NextId);
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

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            var xmlCollection = new XmlLimerCollection()
            {
                NextId = collection.NextId,
                Limers = Mapping.Mapper.Map<List<XmlLimer>>(collection.GetAll())
            };
            using (var fs = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                using(var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(filePath) + ".xml");
                    using(var es = entry.Open())
                    {
                        using (var writer = new StreamWriter(es))
                        {
                            var serilaizer = new XmlSerializer(typeof(XmlLimerCollection));
                            serilaizer.Serialize(writer, xmlCollection);
                        }
                    }
                }
            }
        }
    }
}
