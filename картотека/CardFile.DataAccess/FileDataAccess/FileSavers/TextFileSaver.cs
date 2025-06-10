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
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();

                while (!reader.EndOfStream)
                {
                    var nextLine = reader.ReadLine();
                    if (string.IsNullOrEmpty(nextLine))
                        continue;

                    var lineParts = nextLine.Split('*');
                    if (lineParts.Length != 9)
                        throw new Exception($"В строке выполняется некорректное количество записей");

                    var newRecord = new CardDto();

                    if (int.TryParse(lineParts[0], out var id))
                        newRecord.Id = id;

                    if (int.TryParse(lineParts[1], out var bayNumber))
                        newRecord.BayNumber = bayNumber;

                    newRecord.ClientName = lineParts[2];
                    newRecord.ServiceType = lineParts[3];

                    if (DateTime.TryParse(lineParts[4], out var dropOffDate))
                        newRecord.DropOffDate = dropOffDate;
                    else
                        throw new Exception($"В строке {fileName} неверное значение DropOffDate");

                    if (lineParts[5] == "-")
                        newRecord.PickupDate = null;
                    else if (DateTime.TryParse(lineParts[5], out var pickupDate))
                        newRecord.PickupDate = pickupDate;
                    else
                        throw new Exception($"В строке {fileName} неверное значение PickupDate");

                    if (decimal.TryParse(lineParts[6], out var cost))
                        newRecord.ServiceCost = cost;
                    else
                        throw new Exception($"В строке {fileName} неверное значение ServiceCost");

                    if (bool.TryParse(lineParts[7], out var isPaid))
                        newRecord.IsPaid = isPaid;
                    else
                        throw new Exception($"В строке {fileName} неверное значение IsPaid");

                    newRecord.MechanicName = lineParts[8];

                    records.Add(newRecord);
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id}*{item.BayNumber}*{item.ClientName}*{item.ServiceType}*" +
                        $"{item.DropOffDate.ToShortDateString()}*{item.PickupDate?.ToShortDateString() ?? "-"}*" +
                        $"{item.ServiceCost}*{item.IsPaid}*{item.MechanicName}");
                }
            }
        }
    }
}
