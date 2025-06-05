using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class BinaryFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                var records = new List<CardDto>();

                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newRecord = new CardDto();

                        newRecord.Id = reader.ReadInt32();
                        newRecord.BayNumber = reader.ReadInt32();
                        newRecord.ClientName = reader.ReadString();
                        newRecord.ServiceType = reader.ReadString();

                        var ticks = reader.ReadInt64();
                        newRecord.DropOffDate = new DateTime(ticks);

                        var isPickupDatePresent = reader.ReadBoolean();
                        if (isPickupDatePresent)
                        {
                            ticks = reader.ReadInt64();
                            newRecord.PickupDate = new DateTime(ticks);
                        }

                        newRecord.ServiceCost = reader.ReadDecimal();
                        newRecord.IsPaid = reader.ReadBoolean();
                        newRecord.MechanicName = reader.ReadString();

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

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.BayNumber);
                    writer.Write(item.ClientName ?? "");
                    writer.Write(item.ServiceType ?? "");
                    writer.Write(item.DropOffDate.Ticks);
                    writer.Write(item.PickupDate.HasValue);

                    if (item.PickupDate.HasValue)
                    {
                        writer.Write(item.PickupDate.Value.Ticks);
                    }

                    writer.Write(item.ServiceCost);
                    writer.Write(item.IsPaid);
                    writer.Write(item.MechanicName ?? "");
                }
            }
        }
    }
}
