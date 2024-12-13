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
        public void OpenFile(string fileName, CardFileDataCollection collection)
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
                        Console.WriteLine($"Количество полей: {lineParts.Length}");
                        if (lineParts.Length != 7)
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

                        newRecord.Title = lineParts[1];

                        if (DateTime.TryParse(lineParts[2], out var realeseDate))
                        {
                            newRecord.DateRelease = realeseDate;

                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Daterealese");
                        }

                        
                            newRecord.Director = lineParts[3];
                        
                       

                        newRecord.FilmReuge = lineParts[4];
                        
                        if (decimal.TryParse(lineParts[5], out var price))
                        {
                            newRecord.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение SubordinatesCount");
                        }
                        if (int.TryParse(lineParts[6], out var actor))
                        {
                            newRecord.Count_actor = actor;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение SubordinatesCount");
                        }
                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveFile(string fileName, CardFileDataCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.Title};{item.DateRelease.ToShortDateString()};{item.Director};" +
                            $"{item.FilmReuge};{item.Price};{item.Count_actor}") ;
                           ;
                    }
                }
            }
        }
    }
}
