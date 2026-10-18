using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if (lineParts.Length != 11)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество полей. Ожидалось 11.");
                        }

                        var newCard = new CardDto();

                        newCard.Id = int.Parse(lineParts[0]);

                        newCard.FirstName = lineParts[1];
                        newCard.MiddleName = lineParts[2];
                        newCard.LastName = lineParts[3];

                        newCard.BirthDate = DateTime.Parse(lineParts[4]);

                        newCard.Faculty = lineParts[5];
                        newCard.GroupNumber = lineParts[6];
                        newCard.RecordBookNumber = int.Parse(lineParts[7]);
                        newCard.AverageScore = double.Parse(lineParts[8]);
                        newCard.IsBudget = bool.Parse(lineParts[9]);

                        if (lineParts[10] == "-")
                        {
                            newCard.ExpulsionDate = null;
                        }

                        else
                        {
                            newCard.ExpulsionDate = DateTime.Parse(lineParts[10]);
                        }

                        newCard.PhotoPath = lineParts.Length > 11 && lineParts[11] != "-" ? lineParts[11] : null;

                        records.Add(newCard);

                        collection.ReplaceAll(records);
                    }
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                            $"{item.BirthDate.ToShortDateString()};" +
                            $"{item.Faculty};" +
                            $"{item.GroupNumber};" +
                            $"{item.RecordBookNumber};" +
                            $"{item.AverageScore};" +
                            $"{item.IsBudget};" +
                            $"{(item.ExpulsionDate.HasValue ? item.ExpulsionDate.Value.ToShortDateString() : "-")};" +
                            $"{(string.IsNullOrEmpty(item.PhotoPath) ? "-" : item.PhotoPath)}");
                    }
                }
            }
        }
    }
}
