using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
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

                        var lineParts = nextLine.Split('*');
                        if (lineParts.Length != 9)
                        {
                            throw new Exception($"В строке выполняется некорректное количество записей");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        if (int.TryParse(lineParts[1], out var tableNumber))
                        {
                            newRecord.TableNumber = tableNumber;
                        }
                        newRecord.CustomerName = lineParts[2];
                        newRecord.OrderType = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var orderDate))
                        {
                            newRecord.OrderDate = orderDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение OrderDate");
                        }

                        if (lineParts[5] == "-")
                        {
                            newRecord.CompletionDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[5], out var completionDate))
                        {
                            newRecord.CompletionDate = completionDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение CompletionDate");
                        }

                        if (decimal.TryParse(lineParts[6], out var price))
                        {
                            newRecord.Price = price;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение Price");
                        }

                        if (bool.TryParse(lineParts[7], out var isPaid))
                        {
                            newRecord.IsPaid = isPaid;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение IsPaid");
                        }

                        newRecord.WaiterName = lineParts[8];

                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id}*{item.TableNumber}*{item.CustomerName}*{item.OrderType}*" +
                            $"{item.OrderDate.ToShortDateString()}*{item.CompletionDate?.ToShortDateString() ?? "-"}*" +
                            $"{item.Price}*{item.IsPaid}*{item.WaiterName}");
                    }
                }
            }
        }
    }
}