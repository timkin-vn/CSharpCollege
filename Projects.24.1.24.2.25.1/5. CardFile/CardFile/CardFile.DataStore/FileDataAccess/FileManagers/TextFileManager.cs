using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void SaveToFile(string fileName, StudentCollection collection)
        {
            var students = collection.GetAll().ToList();
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Id}|{student.LastName}|{student.FirstName}|{student.MiddleName}|{student.BirthDate}|{student.Faculty}|{student.Course}|{student.Group}|{student.RecordBookNumber}|{student.AverageGrade}|{student.AdmissionDate}|{student.DismissalDate}");
                }
            }
        }

        public void OpenFromFile(string fileName, StudentCollection collection)
        {
            collection.GetAll().ToList().Clear();
            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 12)
                    {
                        var student = new StudentDto
                        {
                            Id = int.Parse(parts[0]),
                            LastName = parts[1],
                            FirstName = parts[2],
                            MiddleName = parts[3],
                            BirthDate = DateTime.Parse(parts[4]),
                            Faculty = parts[5],
                            Course = int.Parse(parts[6]),
                            Group = parts[7],
                            RecordBookNumber = parts[8],
                            AverageGrade = double.Parse(parts[9]),
                            AdmissionDate = DateTime.Parse(parts[10]),
                            DismissalDate = string.IsNullOrEmpty(parts[11]) ? (DateTime?)null : DateTime.Parse(parts[11])
                        };
                        collection.Save(student);
                    }
                }
            }
        }
    }
}