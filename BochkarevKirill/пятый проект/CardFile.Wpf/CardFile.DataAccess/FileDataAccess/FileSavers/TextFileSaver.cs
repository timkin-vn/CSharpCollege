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
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();

                while (!reader.EndOfStream)
                {
                    var lineParts = reader.ReadLine()?.Split(';');
                    if (lineParts == null || lineParts.Length != 8)
                        throw new Exception($"В строке файла {fileName} неверное количество записей");

                    records.Add(new CardDto
                    {
                        Id = int.Parse(lineParts[0]),
                        FirstName = lineParts[1],
                        MiddleName = lineParts[2],
                        LastName = lineParts[3],
                        ArrivalDate = DateTime.Parse(lineParts[4]),
                        RepairReason = lineParts[5],
                        CompletionDate = lineParts[6] == "-" ? null : (DateTime?)DateTime.Parse(lineParts[6]),
                        RepairCost = decimal.Parse(lineParts[7])
                    });
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
                    writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                        $"{item.ArrivalDate.ToShortDateString()};{item.RepairReason};" +
                        $"{item.CompletionDate?.ToShortDateString() ?? "-"};{item.RepairCost}");
                }
            }
        }
    }
}