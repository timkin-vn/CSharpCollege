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
                        if (lineParts.Length != 7)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество полей");
                        }

                        var newCard = new CardDto();

                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newCard.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Id");
                        }

                        newCard.Title = lineParts[1];
                        newCard.Text = lineParts[2];
                        newCard.Category = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var createdAt))
                        {
                            newCard.CreatedAt = createdAt;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение CreatedAt");
                        }

                        if (bool.TryParse(lineParts[5], out var isDone))
                        {
                            newCard.IsDone = isDone;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение IsDone");
                        }

                        if (bool.TryParse(lineParts[6], out var isPinned))
                        {
                            newCard.IsPinned = isPinned;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение IsPinned");
                        }

                        records.Add(newCard);
                    }

                    collection.ReplaceAll(records);
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
                        writer.WriteLine($"{item.Id};{item.Title};{item.Text};{item.Category};{item.CreatedAt.ToShortDateString()};{item.IsDone};{item.IsPinned}");
                    }
                }
            }
        }
    }
}