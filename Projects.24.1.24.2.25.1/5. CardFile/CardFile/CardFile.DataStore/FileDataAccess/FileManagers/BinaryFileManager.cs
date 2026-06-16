using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class BinaryFileManager : IFileManager
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                var students = new List<StudentDto>(collection.GetAll());
                formatter.Serialize(stream, students);
            }
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var students = (List<StudentDto>)formatter.Deserialize(stream);
                foreach (var student in students)
                {
                    collection.Save(student);
                }
            }
        }
    }
}