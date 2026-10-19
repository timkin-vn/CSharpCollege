using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        private const int FieldCount = 7;

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
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
                    if (lineParts.Length != FieldCount)
                    {
                        throw new Exception($"В строке файла {fileName} неверное количество полей");
                    }

                    var newCard = new CardDto();

                    if (int.TryParse(lineParts[0], out var id))
                        newCard.Id = id;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Id");

                    newCard.Name = lineParts[1];
                    newCard.Industry = lineParts[2];

                    if (int.TryParse(lineParts[3], out var foundationYear))
                        newCard.FoundationYear = foundationYear;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение FoundationYear");

                    if (decimal.TryParse(lineParts[4], out var revenue))
                        newCard.Revenue = revenue;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Revenue");

                    if (int.TryParse(lineParts[5], out var employees))
                        newCard.Employees = employees;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Employees");

                    newCard.Description = lineParts[6];

                    records.Add(newCard);
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    var description = (item.Description ?? string.Empty)
                        .Replace(";", ",")
                        .Replace("\r", " ")
                        .Replace("\n", " ");

                    writer.WriteLine($"{item.Id};{item.Name};{item.Industry};" +
                        $"{item.FoundationYear};{item.Revenue};{item.Employees};{description}");
                }
            }
        }
    }
}