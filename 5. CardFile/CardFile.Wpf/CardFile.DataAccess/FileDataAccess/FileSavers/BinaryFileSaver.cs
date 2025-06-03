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
                        var newRecord = new CardDto();

                        newRecord.Id = reader.ReadInt32();
                        newRecord.FirstName = reader.ReadString();
                        newRecord.MiddleName = reader.ReadString();
                        newRecord.LastName = reader.ReadString();

                        var ticks = reader.ReadInt64();
                        newRecord.BirthDate = new DateTime(ticks);

                        newRecord.Gender = reader.ReadString();
                        newRecord.Address = reader.ReadString();
                        newRecord.Diagnosis = reader.ReadString();

                        bool hasLastVisit = reader.ReadBoolean();
                        if (hasLastVisit)
                        {
                            ticks = reader.ReadInt64();
                            newRecord.LastVisitDate = new DateTime(ticks);
                        }

                        newRecord.PhoneNumber = reader.ReadString();

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
                    writer.Write(item.BirthDate.Ticks);

                    writer.Write(item.Gender);
                    writer.Write(item.Address);
                    writer.Write(item.Diagnosis);

                    writer.Write(item.LastVisitDate.HasValue);
                    if (item.LastVisitDate.HasValue)
                        writer.Write(item.LastVisitDate.Value.Ticks);

                    writer.Write(item.PhoneNumber ?? string.Empty);
                }
            }
        }
    }
}

