using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var inputLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(inputLine))
                        {
                            continue;
                        }

                        var lineSegments = inputLine.Split(';');
                        if (lineSegments.Length != 7)
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineSegments[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        newRecord.CourseNumber = lineSegments[1];
                        newRecord.Tutor = lineSegments[3];
                        newRecord.SpecialProgram = lineSegments[4];

                        if (int.TryParse(lineSegments[5], out var NumberStudents))
                        {
                            newRecord.NumberStudents = NumberStudents;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        records.Add(newRecord);
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.CourseNumber};{item.Tutor};" +
                            $"{item.SpecialProgram};{item.NumberStudents}");
                    }
                }
            }
        }
    }
}
