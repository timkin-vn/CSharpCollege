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
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();

                    while (!reader.EndOfStream) // Исправлено: убраны скобки
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
                        newRecord.LicensePlate = lineParts[1];
                        newRecord.OwnerName = lineParts[2];
                        newRecord.VehicleType = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var entryDate))
                        {
                            newRecord.EntryDate = entryDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение EntryDate");
                        }

                        if (lineParts[5] == "-")
                        {
                            newRecord.ExitDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[5], out var exitDate))
                        {
                            newRecord.ExitDate = exitDate;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение ExitDate");
                        }

                        if (decimal.TryParse(lineParts[6], out var hourlyRate))
                        {
                            newRecord.HourlyRate = hourlyRate;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение HourlyRate");
                        }

                        if (bool.TryParse(lineParts[7], out var isPaid))
                        {
                            newRecord.IsPaid = isPaid;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение IsPaid");
                        }

                        if (int.TryParse(lineParts[8], out var parkingSpot))
                        {
                            newRecord.ParkingSpot = parkingSpot;
                        }
                        else
                        {
                            throw new Exception($"В строке {fileName} неверное значение ParkingSpot");
                        }

                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id}*{item.LicensePlate}*{item.OwnerName}*{item.VehicleType}*" +
                            $"{item.EntryDate.ToShortDateString()}*{item.ExitDate?.ToShortDateString() ?? "-"}*" +
                            $"{item.HourlyRate}*{item.IsPaid}*{item.ParkingSpot}");
                    }
                }
            }
        }
    }
}