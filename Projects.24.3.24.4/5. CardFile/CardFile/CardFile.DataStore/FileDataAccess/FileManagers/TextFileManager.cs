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

                    newCard.ClientFirstName = lineParts[1];
                    newCard.ClientLastName = lineParts[2];
                    newCard.ProductName = lineParts[3];

                    if (DateTime.TryParse(lineParts[4], out var date1))
                        newCard.OrderDate = date1;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение OrderDate");

                    newCard.Address = lineParts[5];
                    newCard.DeliveryMethod = lineParts[6];

                    if (DateTime.TryParse(lineParts[7], out var date2))
                        newCard.ShippingDate = date2;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение ShippingDate");

                    if (lineParts[8] == "-")
                        newCard.ReceivedDate = null;
                    else if (DateTime.TryParse(lineParts[8], out var date3))
                        newCard.ReceivedDate = date3;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение ReceivedDate");

                    if (decimal.TryParse(lineParts[9], out var amount))
                        newCard.TotalAmount = amount;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение TotalAmount");

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
                    writer.WriteLine($"{item.Id};{item.ClientFirstName};{item.ClientLastName};{item.ProductName};" +
                        $"{item.OrderDate.ToShortDateString()};{item.Address};{item.DeliveryMethod};" +
                        $"{item.ShippingDate.ToShortDateString()};{item.ReceivedDate?.ToShortDateString() ?? "-"};" +
                        $"{item.TotalAmount}");
                }
            }
        }
    }
}
