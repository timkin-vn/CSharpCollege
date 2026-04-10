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
                    {
                        continue;
                    }

                    var lineParts = line.Split(';');
                    if (lineParts.Length != 10)
                    {
                        throw new Exception($"В строке файла {fileName} неверное количество полей");
                    }

                    var newCard = new CardDto();

                    if (int.TryParse(lineParts[0], out var id))
                        newCard.Id = id;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Id");

                    newCard.Title = lineParts[1];
                    newCard.Studio = lineParts[2];
                    newCard.Genre = lineParts[3];

                    if (DateTime.TryParse(lineParts[4], out var date1))
                        newCard.ReleaseDate = date1;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение ReleaseDate");

                    newCard.Platform = lineParts[5];
                    newCard.Publisher = lineParts[6];

                    if (DateTime.TryParse(lineParts[7], out var date2))
                        newCard.PurchaseDate = date2;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение PurchaseDate");

                    if (lineParts[8] == "-")
                        newCard.CompletionDate = null;
                    else if (DateTime.TryParse(lineParts[8], out var date3))
                        newCard.CompletionDate = date3;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение CompletionDate");

                    if (decimal.TryParse(lineParts[9], out var amount))
                        newCard.Price = amount;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Price");

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
                    writer.WriteLine($"{item.Id};{item.Title};{item.Studio};{item.Genre};" +
                        $"{item.ReleaseDate.ToShortDateString()};{item.Platform};{item.Publisher};" +
                        $"{item.PurchaseDate.ToShortDateString()};{item.CompletionDate?.ToShortDateString() ?? "-"};" +
                        $"{item.Price}");
                }
            }
        }
    }
}
