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
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();

                    while (!reader.EndOfStream)
                    {
                        var nextLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(nextLine))
                        {
                            continue;
                        }

                        var lineParts = nextLine.Split(';');
                        if (lineParts.Length != 10)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество записей");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Id");
                        }

                        newRecord.DishName = lineParts[1];
                        newRecord.Category = lineParts[2];
                        newRecord.Description = lineParts[3];
                        if (int.TryParse(lineParts[4], out var PortionSize))
                        {
                            newRecord.PortionSize = PortionSize;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение PortionSize");
                        }
                        if (int.TryParse(lineParts[5], out var Price))
                        {
                            newRecord.Price = Price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Price");
                        }
                        
                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.DishName};{item.Category};{item.Description};" +
                            $"{item.PortionSize};{item.Price}");
                    }
                }
            }
        }
    }
}
