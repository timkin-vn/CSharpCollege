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
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();
                int lineNumber = 0;

                while (!reader.EndOfStream)
                {
                    lineNumber++;
                    var nextLine = reader.ReadLine();
                    if (string.IsNullOrEmpty(nextLine))
                    {
                        continue;
                    }

                    try
                    {
                        var lineParts = nextLine.Split(';');
                        if (lineParts.Length != 8)
                        {
                            throw new FormatException("Неверное количество полей в строке");
                        }

                        var newRecord = new CardDto
                        {
                            Id = int.Parse(lineParts[0]),
                            Name = lineParts[1],
                            Manufacturer = lineParts[2],
                            Designer = lineParts[3],
                            ProductionYear = int.Parse(lineParts[4]),
                            Type = lineParts[5],
                            MaxSpeed = double.Parse(lineParts[6]),
                            Gun = lineParts[7],
                            Weight = double.Parse(lineParts[8])
                        };

                        records.Add(newRecord);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Ошибка в строке {lineNumber} файла {fileName}: {ex.Message}");
                    }
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
                        $"{item.Id};" +
                        $"{item.Name};" +
                        $"{item.Manufacturer};" +
                        $"{item.Designer};" +
                        $"{item.ProductionYear};" +
                        $"{item.Type};" +
                        $"{item.MaxSpeed};" +
                        $"{item.Gun};" +
                        $"{item.Weight}");
                }
            }
        }
    }
}
