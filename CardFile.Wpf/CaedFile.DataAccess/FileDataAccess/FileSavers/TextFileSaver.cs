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
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

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
                        if (lineSegments.Length != 7)
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineSegments[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        newRecord.Bank_name = lineSegments[1];

                        if (int.TryParse(lineSegments[2], out var ATM_count))
                        {
                            newRecord.ATM_count = ATM_count;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }
                        
                        newRecord.Street = lineSegments[3];
                        newRecord.House = lineSegments[4];
                        newRecord.City = lineSegments[5];

                        if (Decimal.TryParse(lineSegments[6], out var Money_count))
                        {
                            newRecord.Money_count = Money_count;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (int.TryParse(lineSegments[7], out var Money_limit))
                        {
                            newRecord.Money_limit = Money_limit;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }
                        
                        newRecord.Card_number = lineSegments[8];
                        

                        


                        ////
                        records.Add(newRecord);
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.Bank_name};{item.ATM_count};{item.Street};" +
                            $"{item.House};{item.City};{item.Money_count};{item.Money_limit};{item.Card_number}");
                    }
                }
            }
        }
    }
}
