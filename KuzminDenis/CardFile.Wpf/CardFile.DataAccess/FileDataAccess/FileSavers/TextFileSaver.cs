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

                        newRecord.Category = lineParts[1];
                        newRecord.Manufacturer = lineParts[2];
                        newRecord.Series = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var releaseDate))
                        {
                            newRecord.ReleaseDate = releaseDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение ReleaseDate");
                        }

                        if(decimal.TryParse(lineParts[5], out var price))
                        {
                            newRecord.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Price");
                        }

                        if (int.TryParse(lineParts[6], out var stockQuantity))
                        {
                            newRecord.StockQuantity = stockQuantity;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение StockQuantity");
                        }

                        if (int.TryParse(lineParts[7], out var warrantyMonths))
                        {
                            newRecord.WarrantyMonths = warrantyMonths;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение WarrantyMonths");
                        }

                        if (lineParts[8] == "-")
                        {
                            newRecord.DiscontinuedDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[8], out var discontinuedDate))
                        {
                            newRecord.DiscontinuedDate = discontinuedDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DiscontinuedDate");
                        }

                        newRecord.ProducingCountry = lineParts[9];

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
                        writer.WriteLine($"{item.Id};{item.Category};{item.Manufacturer};{item.Series};" +
                            $"{item.ReleaseDate.ToShortDateString()};{item.Price};{item.StockQuantity};" +
                            $"{item.WarrantyMonths};{item.DiscontinuedDate?.ToShortDateString() ?? "-"};" +
                            $"{item.ProducingCountry}");
                    }
                }
            }
        }
    }
}
