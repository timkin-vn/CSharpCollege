using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();
                int nextId = 1;

                if (!reader.EndOfStream)
                {
                    var firstLine = reader.ReadLine();
                    int.TryParse(firstLine, out nextId);
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var lineParts = line.Split(';');
                    if (lineParts.Length != 8)
                    {
                        throw new Exception($"В строке файла {fileName} неверное количество полей");
                    }

                    var newCard = new CardDto
                    {
                        Id = int.Parse(lineParts[0]),
                        Title = lineParts[1],
                        Director = lineParts[2],
                        ReleaseYear = int.Parse(lineParts[3]),
                        Genre = lineParts[4],
                        DurationMinutes = decimal.Parse(lineParts[5], CultureInfo.InvariantCulture),
                        Rating = int.Parse(lineParts[6]),
                        IsAvailable = bool.Parse(lineParts[7])
                    };

                    records.Add(newCard);
                }

                collection.ReplaceAll(records, nextId);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                writer.WriteLine(collection.NextId);

                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id};{item.Title};{item.Director};{item.ReleaseYear};{item.Genre};{item.DurationMinutes.ToString(CultureInfo.InvariantCulture)};{item.Rating};{item.IsAvailable}");
                }
            }
        }
    }
}
