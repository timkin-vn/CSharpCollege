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

                    var lineParts = nextLine.Split(';');
                    if (lineParts.Length != 9)
                        throw new Exception($"Неверный формат строки в файле {fileName}");

                    var newRecord = new CardDto
                    {
                        Id = int.Parse(lineParts[0]),
                        ProductName = lineParts[1],
                        Category = lineParts[2],
                        Manufacturer = lineParts[3],
                        ProductionDate = DateTime.Parse(lineParts[4]),
                        ShelfLifeDays = int.Parse(lineParts[5]),
                        Price = decimal.Parse(lineParts[6]),
                        QuantityInStock = int.Parse(lineParts[7]),
                        ExpirationDate = lineParts[8] == "-" ? null : (DateTime?)DateTime.Parse(lineParts[8])
                    };

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
                    writer.WriteLine(
                        $"{item.Id};{item.ProductName};{item.Category};{item.Manufacturer};" +
                        $"{item.ProductionDate.ToShortDateString()};{item.ShelfLifeDays};" +
                        $"{item.Price};{item.QuantityInStock};" +
                        $"{item.ExpirationDate?.ToShortDateString() ?? "-"}"
                    );
                }
            }
        }
    }
}