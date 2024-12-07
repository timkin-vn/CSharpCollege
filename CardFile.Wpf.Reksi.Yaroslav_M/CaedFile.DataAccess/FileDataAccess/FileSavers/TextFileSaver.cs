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
        public void Open(string fileName, CardProductsCollection collection)
        {
            var records = new List<CardProductsDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var inputLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(inputLine))
                        {
                            continue;
                        }

                        var lineSegments = inputLine.Split(';');
                        if (lineSegments.Length != 9)
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        var newRecord = new CardProductsDto();
                        if (int.TryParse(lineSegments[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        newRecord.NameProducts = lineSegments[1];
                        newRecord.TypeProducts = lineSegments[2];
                        

                        if (DateTime.TryParse(lineSegments[3], out var manuftDate))
                        {
                            newRecord.DateManufacture = manuftDate;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        } 
                        if (DateTime.TryParse(lineSegments[4], out var dateExp))
                        {
                            newRecord.DateExpiration = dateExp;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (int.TryParse(lineSegments[5], out var countproduct ))
                        {
                            newRecord.CountProducts = countproduct;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (decimal.TryParse(lineSegments[6], out var priceoneproducts))
                        {
                            newRecord.PriceOneProducts = priceoneproducts;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        newRecord.SectionProducts = lineSegments[7];
                        newRecord.ShirtProducts = lineSegments[8];

                        records.Add(newRecord);
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardProductsCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.NameProducts};{item.TypeProducts};{item.DateManufacture.ToShortDateString()};" +
                            $"{item.DateExpiration.ToShortDateString()};{item.CountProducts};{item.PriceOneProducts};" +
                            $"{item.SectionProducts}-{item.ShirtProducts};");
                    }
                }
            }
        }
    }
}
