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
                    if (lineParts.Length != 11)
                        throw new Exception($"В строке файла {fileName} неверное количество полей");

                    var newCard = new CardDto
                    {
                        Id = int.Parse(lineParts[0]),
                        FirstName = lineParts[1],
                        MiddleName = lineParts[2],
                        LastName = lineParts[3],
                        BirthDate = DateTime.Parse(lineParts[4]),
                        DeathDate = DateTime.Parse(lineParts[5]),
                        CauseOfDeath = lineParts[6],
                        PlaceOfDeath = lineParts[7],
                        AdmissionDate = DateTime.Parse(lineParts[8]),
                        SectionNumber = lineParts[9],
                        IsReleased = bool.Parse(lineParts[10])
                    };

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
                    writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                                     $"{item.BirthDate.ToShortDateString()};{item.DeathDate.ToShortDateString()};" +
                                     $"{item.CauseOfDeath};{item.PlaceOfDeath};{item.AdmissionDate.ToShortDateString()};" +
                                     $"{item.SectionNumber};{item.IsReleased}");
                }
            }
        }
    }
}