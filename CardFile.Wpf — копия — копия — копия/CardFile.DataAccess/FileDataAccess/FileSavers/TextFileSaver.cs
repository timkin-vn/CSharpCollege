using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardFileDataCollection collection)
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
                        if (lineParts.Length != 9)
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

                        newRecord.FirstName = lineParts[1];
                        newRecord.MiddleName = lineParts[2];
                        newRecord.LastName = lineParts[3];
                        if (DateTime.TryParse(lineParts[4], out var birthDate))
                        {
                            newRecord.BirthDate = birthDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение BirthDate");
                        }

                        if (decimal.TryParse(lineParts[5], out var paymentAmount))
                        {
                            newRecord.PaymentAmount = paymentAmount;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение PaymentAmount");
                        }

                        if (decimal.TryParse(lineParts[6], out var debtAmount))
                        {
                            newRecord.DebtAmount = debtAmount;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DebtAmount");
                        }

                        newRecord.Position = lineParts[7];
                        if (int.TryParse(lineParts[8], out var subordinatesCount))
                        {
                            newRecord.SubordinatesCount = subordinatesCount;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение SubordinatesCount");
                        }

                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveFile(string fileName, CardFileDataCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                            $"{item.BirthDate.ToShortDateString()};{item.PaymentAmount};{item.DebtAmount};" +
                            $"{item.Position};{item.SubordinatesCount}");
                    }
                }
            }
        }
    }
}
