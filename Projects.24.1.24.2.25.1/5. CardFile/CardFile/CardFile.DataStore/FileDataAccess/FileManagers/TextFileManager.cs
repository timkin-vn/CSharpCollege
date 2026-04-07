using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
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

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        continue;

                    var lineParts = line.Split(';');
                    if (lineParts.Length != 9)
                        throw new Exception($"В строке файла {fileName} неверное количество полей (ожидается 9)");

                    var newCard = new CardDto();

                    // Id
                    if (!int.TryParse(lineParts[0], out var id))
                        throw new Exception($"В строке файла {fileName} неверное значение Id");
                    newCard.Id = id;

                    // Nickname
                    newCard.Nickname = Unescape(lineParts[1]);
                    // RealName
                    newCard.RealName = Unescape(lineParts[2]);
                    // Country
                    newCard.Country = Unescape(lineParts[3]);

                    // BirthDate
                    if (!DateTime.TryParse(lineParts[4], out var birthDate))
                        throw new Exception($"В строке файла {fileName} неверное значение BirthDate");
                    newCard.BirthDate = birthDate;

                    // Team
                    newCard.Team = Unescape(lineParts[5]);
                    // Role
                    newCard.Role = Unescape(lineParts[6]);

                    // TotalEarnings
                    if (!decimal.TryParse(lineParts[7], out var totalEarnings))
                        throw new Exception($"В строке файла {fileName} неверное значение TotalEarnings");
                    newCard.TotalEarnings = totalEarnings;

                    // Achievements
                    newCard.Achievements = Unescape(lineParts[8]);

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
                    writer.WriteLine($"{item.Id};{Escape(item.Nickname)};{Escape(item.RealName)};{Escape(item.Country)};" +
                        $"{item.BirthDate.ToShortDateString()};{Escape(item.Team)};{Escape(item.Role)};" +
                        $"{item.TotalEarnings};{Escape(item.Achievements)}");
                }
            }
        }

        // Замена разделителя ';' внутри строк на запятую, чтобы не ломать формат
        private string Escape(string s) => s?.Replace(';', ',') ?? "";

        // Обратное преобразование
        private string Unescape(string s) => s?.Replace(',', ';') ?? "";
    }
}