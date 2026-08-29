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
                        var newRecord = new CardDto();

                        newRecord.Id = reader.ReadInt32();
                        newRecord.Brand = reader.ReadString();
                        newRecord.Model = reader.ReadString();
                        newRecord.Year = reader.ReadInt32();
                        newRecord.Vin = reader.ReadString();
                        newRecord.Color = reader.ReadString();
                        newRecord.Price = reader.ReadDecimal();

                        long ticks = reader.ReadInt64();
                        newRecord.ArrivalDate = new DateTime(ticks);

                        bool hasSaleDate = reader.ReadBoolean();
                        if (hasSaleDate)
                        {
                            ticks = reader.ReadInt64();
                            newRecord.SaleDate = new DateTime(ticks);
                        }

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
                    writer.Write(item.Brand);
                    writer.Write(item.Model);
                    writer.Write(item.Year);
                    writer.Write(item.Vin);
                    writer.Write(item.Color);
                    writer.Write(item.Price);
                    writer.Write(item.ArrivalDate.Ticks);

                    writer.Write(item.SaleDate.HasValue);
                    if (item.SaleDate.HasValue)
                    {
                        writer.Write(item.SaleDate.Value.Ticks);
                    }
                }
            }
        }
    }
}
