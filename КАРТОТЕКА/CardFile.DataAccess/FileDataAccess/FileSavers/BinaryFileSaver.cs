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
                            newRecord.LicensePlate = reader.ReadString();
                            newRecord.OwnerName = reader.ReadString();
                            newRecord.VehicleType = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.EntryDate = new DateTime(ticks);

                            var isExitDatePresent = reader.ReadBoolean();
                            if (isExitDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newRecord.ExitDate = new DateTime(ticks);
                            }

                            newRecord.HourlyRate = reader.ReadDecimal();
                            newRecord.IsPaid = reader.ReadBoolean();
                            newRecord.ParkingSpot = reader.ReadInt32();

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

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.LicensePlate ?? "");
                        writer.Write(item.OwnerName ?? "");
                        writer.Write(item.VehicleType ?? "");
                        writer.Write(item.EntryDate.Ticks);
                        writer.Write(item.ExitDate.HasValue);

                        if (item.ExitDate.HasValue)
                        {
                            writer.Write(item.ExitDate.Value.Ticks);
                        }

                        writer.Write(item.HourlyRate);
                        writer.Write(item.IsPaid);
                        writer.Write(item.ParkingSpot);
                    }
                }
            }
        }
    }
}