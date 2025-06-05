using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class BinaryFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                var records = new List<CardDto>();

                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newRecord = new CardDto
                        {
                            Id = reader.ReadInt32(),
                            FirstName = reader.ReadString(),
                            MiddleName = reader.ReadString(),
                            LastName = reader.ReadString(),
                            ArrivalDate = new DateTime(reader.ReadInt64()),
                            RepairReason = reader.ReadString(),
                            CompletionDate = reader.ReadBoolean() ? new DateTime(reader.ReadInt64()) : (DateTime?)null,
                            RepairCost = reader.ReadDecimal()
                        };
                        records.Add(newRecord);
                    }
                }
                catch
                {
                    throw new Exception($"Неверные данные в файле {fileName}");
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.FirstName);
                    writer.Write(item.MiddleName);
                    writer.Write(item.LastName);
                    writer.Write(item.ArrivalDate.Ticks);
                    writer.Write(item.RepairReason);
                    writer.Write(item.CompletionDate.HasValue);
                    if (item.CompletionDate.HasValue)
                    {
                        writer.Write(item.CompletionDate.Value.Ticks);
                    }
                    writer.Write(item.RepairCost);
                }
            }
        }
    }
}