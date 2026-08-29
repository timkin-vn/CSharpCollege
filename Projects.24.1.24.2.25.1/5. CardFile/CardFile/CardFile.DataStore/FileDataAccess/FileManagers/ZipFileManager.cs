using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text.Json;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class ZipFileManager : IFileManager
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var json = JsonSerializer.Serialize(collection.GetAll());
            var tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, json);

            using (var zip = ZipFile.Open(fileName, ZipArchiveMode.Create))
            {
                zip.CreateEntryFromFile(tempFile, "data.json");
            }

            File.Delete(tempFile);
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            using (var zip = ZipFile.OpenRead(fileName))
            {
                var entry = zip.GetEntry("data.json");
                using (var stream = entry.Open())
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    var students = JsonSerializer.Deserialize<List<StudentDto>>(json);
                    collection.ReplaceAll(students);
                }
            }
        }
    }
}