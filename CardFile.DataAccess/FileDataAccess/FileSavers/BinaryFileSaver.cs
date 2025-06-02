using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            Name = reader.ReadString(),
                            Manufacturer = reader.ReadString(),
                            Designer = reader.ReadString(),
                            ProductionYear = reader.ReadInt32(),
                            Type = reader.ReadString(),
                            MaxSpeed = reader.ReadDouble(),
                            Gun = reader.ReadString(),
                            Weight = reader.ReadDouble()
                        };

                        records.Add(newRecord);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка чтения файла {fileName}: {ex.Message}");
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
                    writer.Write(item.Name);
                    writer.Write(item.Manufacturer);
                    writer.Write(item.Designer);
                    writer.Write(item.ProductionYear);
                    writer.Write(item.Type);
                    writer.Write(item.MaxSpeed);
                    writer.Write(item.Gun);
                    writer.Write(item.Weight);
                }
            }
        }
    }
}