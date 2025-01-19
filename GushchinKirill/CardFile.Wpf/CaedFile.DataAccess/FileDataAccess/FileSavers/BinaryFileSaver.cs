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
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newRecord = new CardDto();

                        try
                        {
                            newRecord.Id = reader.ReadInt32();
                            newRecord.Name = reader.ReadString();
                            newRecord.Description = reader.ReadString();
                            newRecord.Street = reader.ReadString();
                            newRecord.House = reader.ReadString();
                            newRecord.City = reader.ReadString();
                            newRecord.MailIndex = reader.ReadInt32();
                            newRecord.Rating = reader.ReadDouble();
                            newRecord.CounterReviews = reader.ReadInt32();
                            newRecord.Status = reader.ReadString();
                        }
                        catch
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        records.Add(newRecord);
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.Name);
                        writer.Write(item.Description);
                        writer.Write(item.Street);
                        writer.Write(item.House);
                        writer.Write(item.City);
                        writer.Write(item.MailIndex);
                        writer.Write(item.Rating);
                        writer.Write(item.CounterReviews);
                        writer.Write(item.Status);
                    }
                }
            }
        }
    }
}
