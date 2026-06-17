using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs, Encoding.UTF8))
            {
                var records = new List<CardDto>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var parts = line.Split(';');
                    if (parts.Length != 7)
                        throw new Exception($"В строке файла {fileName} неверное количество полей. Ожидалось 7.");

                    var newMovie = new CardDto();

                    if (int.TryParse(parts[0], out var id)) newMovie.Id = id;
                    else throw new Exception("Неверный Id фильма");

                    newMovie.Title = parts[1];
                    newMovie.Director = parts[2];

                    if (int.TryParse(parts[3], out var year)) newMovie.Year = year;
                    else throw new Exception("Неверный год выпуска");

                    newMovie.Genre = parts[4];

                    if (int.TryParse(parts[5], out var duration)) newMovie.Duration = duration;
                    else throw new Exception("Неверная длительность фильма");

                    if (decimal.TryParse(parts[6], out var rating)) newMovie.Rating = rating;
                    else throw new Exception("Неверный рейтинг фильма");

                    records.Add(newMovie);
                }
                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs, Encoding.UTF8))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.Title};{item.Director};{item.Year};{item.Genre};{item.Duration};{item.Rating}");
                    }
                }
            }
        }

    }
}
