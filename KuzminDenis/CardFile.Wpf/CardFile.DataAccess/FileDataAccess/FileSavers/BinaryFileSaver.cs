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
                            newRecord.Category = reader.ReadString();
                            newRecord.Manufacturer = reader.ReadString();
                            newRecord.Series = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.ReleaseDate = new DateTime(ticks);

                            newRecord.Price = reader.ReadInt32();
                            newRecord.StockQuantity = reader.ReadInt32();

                            newRecord.WarrantyMonths = reader.ReadInt32();

                            var isDiscontinuedDatePresent = reader.ReadBoolean();
                            if (isDiscontinuedDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newRecord.DiscontinuedDate = new DateTime(ticks);
                            }

                            newRecord.ProducingCountry = reader.ReadString();

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
                        writer.Write(item.Category);
                        writer.Write(item.Manufacturer);
                        writer.Write(item.Series);
                        writer.Write(item.ReleaseDate.Ticks);
                        writer.Write(item.Price);
                        writer.Write(item.StockQuantity);
                        writer.Write(item.WarrantyMonths);
                        writer.Write(item.DiscontinuedDate.HasValue);

                        if (item.DiscontinuedDate.HasValue)
                        {
                            writer.Write(item.DiscontinuedDate.Value.Ticks);
                        }

                        writer.Write(item.ProducingCountry);
                    }
                }
            }
        }
    }
}
