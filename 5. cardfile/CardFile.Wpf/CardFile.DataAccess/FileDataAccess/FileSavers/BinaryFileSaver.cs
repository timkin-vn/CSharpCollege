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
                            ProductName = reader.ReadString(),
                            Category = reader.ReadString(),
                            Manufacturer = reader.ReadString(),
                            ProductionDate = new DateTime(reader.ReadInt64()),
                            ShelfLifeDays = reader.ReadInt32(),
                            Price = reader.ReadDecimal(),
                            QuantityInStock = reader.ReadInt32(),
                            ExpirationDate = reader.ReadBoolean() ? new DateTime(reader.ReadInt64()) : (DateTime?)null
                        };

                        records.Add(newRecord);
                    }
                }
                catch
                {
                    throw new Exception($"Ошибка чтения бинарного файла {fileName}");
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
                    writer.Write(item.ProductName);
                    writer.Write(item.Category);
                    writer.Write(item.Manufacturer);
                    writer.Write(item.ProductionDate.Ticks);
                    writer.Write(item.ShelfLifeDays);
                    writer.Write(item.Price);
                    writer.Write(item.QuantityInStock);
                    writer.Write(item.ExpirationDate.HasValue);

                    if (item.ExpirationDate.HasValue)
                        writer.Write(item.ExpirationDate.Value.Ticks);
                }
            }
        }
    }
}