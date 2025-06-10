using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                while (!reader.EndOfStream)
                {
                    var inputLine = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(inputLine)) continue;

                    var lineSegments = inputLine.Split(';');
                    if (lineSegments.Length != 8)
                        throw new Exception($"Неверный формат файла {fileName}");

                    var newRecord = new CardDto();

                    if (!int.TryParse(lineSegments[0], out var id))
                        throw new Exception($"Неверный Id в файле {fileName}");
                    newRecord.Id = id;

                    newRecord.LaptopModel = lineSegments[1];

                    if (!decimal.TryParse(lineSegments[2], out var price))
                        throw new Exception($"Неверная цена в файле {fileName}");
                    newRecord.LaptopPrice = price;

                    newRecord.VideoCard = lineSegments[3];
                    newRecord.Processor = lineSegments[4];
                    newRecord.Storage = lineSegments[5];
                    newRecord.Ram = lineSegments[6];
                    newRecord.Warranty = lineSegments[7];

                    records.Add(newRecord);
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var writer = new StreamWriter(fileName))
            {
                writer.WriteLine("ID  | Model                    | Price     | VideoCard        | Processor       | Storage | RAM  | Warranty");
                writer.WriteLine(new string('-', 90));

                foreach (var item in collection.GetAll())
                {
                    var line = $"{item.Id,3} | {item.LaptopModel,-24} | {item.LaptopPrice,9:N2} | {item.VideoCard,-16} | {item.Processor,-15} | {item.Storage,-7} | {item.Ram,-4} | {item.Warranty}";
                    writer.WriteLine(line);
                }
            }
        }
    }
}
