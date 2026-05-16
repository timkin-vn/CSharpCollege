using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class XmlFileManager : IFileManager
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var serializer = new XmlSerializer(typeof(List<StudentDto>));
            using (var writer = new StreamWriter(fileName))
            {
                var students = new List<StudentDto>(collection.GetAll());
                serializer.Serialize(writer, students);
            }
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            var serializer = new XmlSerializer(typeof(List<StudentDto>));
            using (var reader = new StreamReader(fileName))
            {
                var students = (List<StudentDto>)serializer.Deserialize(reader);
                foreach (var student in students)
                {
                    collection.Save(student);
                }
            }
        }
    }
}