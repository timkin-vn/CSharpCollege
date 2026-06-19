using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;
using TriangleEditor.Business.Models;

namespace TriangleEditor.Business.Services
{
   
    public class FileService
    {
        private const string EntryName = "picture.xml";

        public void Save(PictureModel picture, string path)
        {
            var serializer = new XmlSerializer(typeof(PictureModel));

            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
            {
                var entry = archive.CreateEntry(EntryName, CompressionLevel.Optimal);
                using (var entryStream = entry.Open())
                {
                    serializer.Serialize(entryStream, picture);
                }
            }
        }

        public PictureModel Load(string path)
        {
            var serializer = new XmlSerializer(typeof(PictureModel));

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read))
            {
                var entry = archive.GetEntry(EntryName);
                if (entry == null)
                    return new PictureModel();

                using (var entryStream = entry.Open())
                {
                    return (PictureModel)serializer.Deserialize(entryStream);
                }
            }
        }
    }
}
