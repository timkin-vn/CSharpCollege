using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();

                while (!reader.EndOfStream)
                {
                    var nextLine = reader.ReadLine();
                    if (string.IsNullOrEmpty(nextLine))
                        continue;

                    var parts = nextLine.Split(';');
                    if (parts.Length != 9)
                        throw new Exception($"В строке файла {fileName} неверное количество записей");

                    var newRecord = new CardDto();
                    newRecord.Id = int.Parse(parts[0]);
                    newRecord.Brand = parts[1];
                    newRecord.Model = parts[2];
                    newRecord.Year = int.Parse(parts[3]);
                    newRecord.Vin = parts[4];
                    newRecord.Color = parts[5];
                    newRecord.Price = decimal.Parse(parts[6]);
                    newRecord.ArrivalDate = DateTime.Parse(parts[7]);
                    if (parts[8] == "-")
                    {
                        newRecord.SaleDate = null;
                    }
                    else
                    {
                        newRecord.SaleDate = DateTime.Parse(parts[8]);
                    }

                    records.Add(newRecord);
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id};{item.Brand};{item.Model};{item.Year};{item.Vin};{item.Color};{item.Price};{item.ArrivalDate.ToShortDateString()};{(item.SaleDate.HasValue ? item.SaleDate.Value.ToShortDateString() : "-")}");
                }
            }
        }
    }
}
