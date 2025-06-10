using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class BinaryFileSaver : IFileSaver
    {
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    var newRecord = new CardDto();

                    try
                    {
                        newRecord.Id = reader.ReadInt32();
                        newRecord.LaptopModel = reader.ReadString();
                        newRecord.LaptopPrice = reader.ReadDecimal();
                        newRecord.VideoCard = reader.ReadString();
                        newRecord.Processor = reader.ReadString();
                        newRecord.Storage = reader.ReadString();
                        newRecord.Ram = reader.ReadString();
                        newRecord.Warranty = reader.ReadString();
                    }
                    catch
                    {
                        throw new Exception($"Неверный формат файла {fileName}");
                    }

                    records.Add(newRecord);
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.LaptopModel ?? "");
                    writer.Write(item.LaptopPrice);
                    writer.Write(item.VideoCard ?? "");
                    writer.Write(item.Processor ?? "");
                    writer.Write(item.Storage ?? "");
                    writer.Write(item.Ram ?? "");
                    writer.Write(item.Warranty ?? "");
                }
            }
        }
    }
}
