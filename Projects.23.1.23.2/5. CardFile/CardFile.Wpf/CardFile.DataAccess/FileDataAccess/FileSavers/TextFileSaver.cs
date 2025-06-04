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
                        if (lineParts.Length != 6)
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

                        newRecord.Name = lineParts[1];

                        if (DateTime.TryParse(lineParts[2], out var deliverDate))
                        {
                            newRecord.DeliverDate = deliverDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DeliverDate");
                        }

                        if (DateTime.TryParse(lineParts[3], out var expirationDate))
                        {
                            newRecord.ExpirationDate = expirationDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение ExpirationDate");
                        }

                        if (int.TryParse(lineParts[4], out var count))
                        {
                            newRecord.Count = count;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Count");
                        }

                        if (double.TryParse(lineParts[5], out var rating))
                        {
                            newRecord.Rating = rating;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Rating");
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
                        writer.WriteLine($"{item.Id};{item.Name};" +
                            $"{item.DeliverDate.ToShortDateString()};" +
                            $"{item.ExpirationDate.ToShortDateString()};" +
                            $"{item.Count};{item.Rating}");
                    }
                }
            }
        }
    }
}
