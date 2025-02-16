using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.StorageEntities;
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
        public void Open(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var entryStream = entry.Open())
                    {
                        using (var reader = new StreamReader(entryStream))
                        {
                            var serializer = new XmlSerializer(typeof(XmlCardCollection));
                            var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);
                            xmlCollection.SaveToCollection(collection);
                        }
                    }
                }
            }
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    var entry = archive.CreateEntry(Path.GetFileNameWithoutExtension(fileName) + ".xml");
                    using (var entryStream = entry.Open())
                    {
                        using (var writer = new StreamWriter(entryStream))
                        {
                            var xmlCollection = new XmlCardCollection();
                            xmlCollection.FillFromCollection(collection);

                            var serializer = new XmlSerializer(typeof(XmlCardCollection));
                            serializer.Serialize(writer, xmlCollection);
                        }
                    }
                }
            }
        }
    }
}
