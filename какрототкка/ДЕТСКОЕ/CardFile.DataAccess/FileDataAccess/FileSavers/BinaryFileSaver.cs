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
                            newRecord.TableNumber = reader.ReadInt32();
                            newRecord.CustomerName = reader.ReadString();
                            newRecord.OrderType = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.OrderDate = new DateTime(ticks);

                            var isCompletionDatePresent = reader.ReadBoolean();
                            if (isCompletionDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newRecord.CompletionDate = new DateTime(ticks);
                            }

                            newRecord.Price = reader.ReadDecimal();
                            newRecord.IsPaid = reader.ReadBoolean();
                            newRecord.WaiterName = reader.ReadString();

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

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.TableNumber);
                        writer.Write(item.CustomerName ?? "");
                        writer.Write(item.OrderType ?? "");
                        writer.Write(item.OrderDate.Ticks);
                        writer.Write(item.CompletionDate.HasValue);

                        if (item.CompletionDate.HasValue)
                        {
                            writer.Write(item.CompletionDate.Value.Ticks);
                        }

                        writer.Write(item.Price);
                        writer.Write(item.IsPaid);
                        writer.Write(item.WaiterName ?? "");
                    }
                }
            }
        }
    }
}