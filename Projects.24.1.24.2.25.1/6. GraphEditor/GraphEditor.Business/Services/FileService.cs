using GraphEditor.Business.Models;
using GraphEditor.Business.Models.Xml;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Serialization;

namespace GraphEditor.Business.Services
{
    internal class FileService
    {
        private const string PictureEntryName = "picture.xml";

        public void Save(PictureModel model, string fileName)
        {
            var xml = XmlPicture.ToXml(model);
            var serializer = new XmlSerializer(typeof(XmlPicture));

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            using (var archive = ZipFile.Open(fileName, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(PictureEntryName);

                using (var stream = entry.Open())
                {
                    serializer.Serialize(stream, xml);
                }
            }
        }

        public PictureModel Open(string fileName)
        {
            var serializer = new XmlSerializer(typeof(XmlPicture));

            try
            {
                using (var archive = ZipFile.OpenRead(fileName))
                {
                    var entry = archive.Entries.FirstOrDefault(e =>
                        string.Equals(e.FullName, PictureEntryName, StringComparison.OrdinalIgnoreCase))
                        ?? archive.Entries.FirstOrDefault(e => e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        ?? archive.Entries.FirstOrDefault();

                    if (entry == null)
                    {
                        return new PictureModel();
                    }

                    using (var stream = entry.Open())
                    {
                        return XmlPicture.FromXml((XmlPicture)serializer.Deserialize(stream));
                    }
                }
            }
            catch (InvalidDataException)
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return XmlPicture.FromXml((XmlPicture)serializer.Deserialize(stream));
                }
            }
        }
    }
}
