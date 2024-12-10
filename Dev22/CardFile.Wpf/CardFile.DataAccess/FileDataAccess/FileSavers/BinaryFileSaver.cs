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
        public void OpenFile(string fileName, CardFileDataCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<CardDto>();

                    try
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var newRecord = new CardDto();

                            newRecord.Id = reader.ReadInt32();
                            newRecord.FirstName = reader.ReadString();
                            newRecord.MiddleName = reader.ReadString();
                            newRecord.LastName = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.BirthDate = new DateTime(ticks);

                            newRecord.PaymentAmount = reader.ReadDecimal();
                            newRecord.Department = reader.ReadString();
                            newRecord.Position = reader.ReadString();
                            newRecord.SubordinatesCount = reader.ReadInt32();

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
        }

        public void SaveFile(string fileName, CardFileDataCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.FirstName);
                        writer.Write(item.MiddleName);
                        writer.Write(item.LastName);
                        writer.Write(item.BirthDate.Ticks);
                        writer.Write(item.PaymentAmount);
                        writer.Write(item.Department);
                        writer.Write(item.Position);
                        writer.Write(item.SubordinatesCount);
                    }
                }
            }
        }
    }
}
