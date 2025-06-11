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
                        if (lineParts.Length != 10)
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
                        newCard.Type = lineParts[2];
                        newCard.Manufacturer = lineParts[3];


                        if (decimal.TryParse(lineParts[4], out var price))
                        {
                            newCard.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Salary");
                        }

                        if (int.TryParse(lineParts[4], out var quantity))
                        {
                            newCard.StockQuantity = quantity;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Salary");
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
                        writer.WriteLine($"{item.Id};{item.Name};{item.Type};{item.Manufacturer};" +
                            $"{item.Price};{item.StockQuantity};");
                    }
                }
            }
        }
    }
}
