using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

                        newRecord.BookName = lineParts[1];
                        newRecord.AuthorFirstName = lineParts[2];
                        newRecord.AuthorLastName = lineParts[3];
                        newRecord.Genre = lineParts[4];

                        if (DateTime.TryParse(lineParts[5], out var dateOfPublishing))
                        {
                            newRecord.DateOfPublishing = dateOfPublishing;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение dateOfPublishing");
                        }

                        newRecord.Edition = lineParts[6];
                        if (int.TryParse(lineParts[7], out var price))
                        {
                            newRecord.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Price");
                        }

                        if (int.TryParse(lineParts[8], out var amountOfPages))
                        {
                            newRecord.AmountOfPages = amountOfPages;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение AmountOfPages");
                        }

                        if (lineParts[9] == "-")
                        {
                            newRecord.DateOfDelisting = null;
                        }
                        else if (DateTime.TryParse(lineParts[8], out var dateOfDelisting))
                        {
                            newRecord.DateOfDelisting = dateOfDelisting;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DateOfDelisting");
                        }

                        if (decimal.TryParse(lineParts[9], out var salary))
                        {
                            newRecord.Rating = salary;
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
                        writer.WriteLine($"{item.Id};{item.BookName};{item.AuthorFirstName};{item.AuthorLastName};" +
                            $"{item.Genre};{item.DateOfPublishing.ToShortDateString()};{item.Edition};" +
                            $"{item.Price};{item.AmountOfPages};{item.DateOfDelisting?.ToShortDateString() ?? "-"};" +
                            $"{item.Rating}");
                    }
                }
            }
        }
    }
}
