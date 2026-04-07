using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                var records = new List<LetterDto>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;
                    var parts = line.Split(';');
                    if (parts.Length != 7) throw new Exception($"Неверное количество полей в строке файла {fileName}");

                    var newLetter = new LetterDto();
                    if (!int.TryParse(parts[0], out var id)) throw new Exception($"Неверный Id в файле {fileName}");
                    newLetter.Id = id;
                    newLetter.Sender = parts[1];
                    newLetter.Receiver = parts[2];
                    newLetter.Subject = parts[3];
                    newLetter.Body = parts[4];
                    if (!DateTime.TryParse(parts[5], out var date)) throw new Exception($"Неверная дата в файле {fileName}");
                    newLetter.Date = date;
                    if (!bool.TryParse(parts[6], out var isRead)) throw new Exception($"Неверный статус в файле {fileName}");
                    newLetter.IsRead = isRead;

                    records.Add(newLetter);
                }
                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id};{item.Sender};{item.Receiver};{item.Subject};{item.Body};{item.Date.ToShortDateString()};{item.IsRead}");
                }
            }
        }
    }
}