﻿using CardFile.DataAccess.DataCollection;
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
                            newRecord.FirstName = reader.ReadString();
                            newRecord.MiddleName = reader.ReadString();
                            newRecord.LastName = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.BirthDate = new DateTime(ticks);

                            newRecord.HeightAmount = reader.ReadDecimal();
                            newRecord.Weight = reader.ReadInt32();
                            newRecord.City = reader.ReadString();
                            newRecord.Street = reader.ReadString();
                            newRecord.House = reader.ReadInt32();
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
                        writer.Write(item.FirstName);
                        writer.Write(item.MiddleName);
                        writer.Write(item.LastName);
                        writer.Write(item.BirthDate.Ticks);
                        writer.Write(item.HeightAmount);
                        writer.Write(item.Weight);
                        writer.Write(item.City);
                        writer.Write(item.Street);
                        writer.Write(item.House);
                    }
                }
            }
        }
    }
}
