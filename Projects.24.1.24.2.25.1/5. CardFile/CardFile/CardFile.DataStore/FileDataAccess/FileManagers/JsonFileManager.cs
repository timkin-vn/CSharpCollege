using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class JsonFileManager : IFileManager
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var json = JsonConvert.SerializeObject(collection.GetAll(), Formatting.Indented);
            File.WriteAllText(fileName, json);
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            var json = File.ReadAllText(fileName);
            var students = JsonConvert.DeserializeObject<List<StudentDto>>(json);
            foreach (var student in students)
            {
                collection.Save(student);
            }
        }
    }
}