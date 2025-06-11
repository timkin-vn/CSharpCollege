using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
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
                        if (lineParts.Length != 6)
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

                        newCard.Name = lineParts[1];

                        if (DateTime.TryParse(lineParts[2], out var yearOfProductDate))
                        {
                            newCard.YearOfProduction = yearOfProductDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение YearOfProduction");
                        }

                        newCard.Type = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var deliveryDate))
                        {
                            newCard.DeliveryDate = deliveryDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DeliveryDate");
                        }


                        if (int.TryParse(lineParts[5], out var price))
                        {
                            newCard.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Price");
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
                        writer.WriteLine($"{item.Id};{item.Name};" +
                            $"{item.YearOfProduction.ToShortDateString()};{item.Type};" +
                            $"{item.DeliveryDate.ToShortDateString()};" +
                            $"{item.Price}");
                    }
                }
            }
        }
    }
}
