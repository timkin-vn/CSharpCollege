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
        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                // Сначала запишем NextId первой строкой
                writer.WriteLine($"#NextId:{collection.NextId}");

                foreach (var item in collection.GetAll())
                {
                    // Формат: Id;Название;Жанр;Рейтинг;Оценка;Дата;Описание
                    writer.WriteLine($"{item.Id};{item.Title};{item.Genre};" +
                                     $"{item.GlobalRating};{item.MyScore};" +
                                     $"{item.ReleaseDate.ToShortDateString()};{item.Description}");
                }
            }
        }

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            if (!File.Exists(fileName)) return;

            var records = new List<CardDto>();
            int nextId = 1;

            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Читаем NextId из служебной строки
                    if (line.StartsWith("#NextId:"))
                    {
                        int.TryParse(line.Replace("#NextId:", ""), out nextId);
                        continue;
                    }

                    var parts = line.Split(';');
                    if (parts.Length < 6) continue;

                    try
                    {
                        records.Add(new CardDto
                        {
                            Id = int.Parse(parts[0]),
                            Title = parts[1],
                            Genre = parts[2],
                            GlobalRating = double.Parse(parts[3]),
                            MyScore = int.Parse(parts[4]),
                            ReleaseDate = DateTime.Parse(parts[5]),
                            Description = parts.Length > 6 ? parts[6] : ""
                        });
                    }
                    catch { /* Пропускаем битые строки */ }
                }
            }
            collection.ReplaceAll(records, nextId);
        }
    }
}
