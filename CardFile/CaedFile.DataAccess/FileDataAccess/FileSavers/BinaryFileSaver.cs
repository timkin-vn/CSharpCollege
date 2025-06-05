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
        public void Open(string fileName, CardProductsCollection collection)
        {
            var records = new List<CardProductsDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newRecord = new CardProductsDto();

                        try
                        {
                            newRecord.Id = reader.ReadInt32();
                            newRecord.NameProducts = reader.ReadString();
                            newRecord.TypeProducts = reader.ReadString();


                            var ticksMaf = reader.ReadInt64();
                            newRecord.DateManufacture = new DateTime(ticksMaf);
                            var ticksExp = reader.ReadInt64();
                            newRecord.DateExpiration = new DateTime(ticksExp);

                            newRecord.CountProducts = reader.ReadInt32();
                            newRecord.PriceOneProducts = reader.ReadDecimal();
                            newRecord.SectionProducts = reader.ReadString();
                            newRecord.ShirtProducts = reader.ReadString();
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

        public void Save(string fileName, CardProductsCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.NameProducts);
                        writer.Write(item.TypeProducts);
                        writer.Write(item.DateManufacture.Ticks);
                        writer.Write(item.DateExpiration.Ticks);
                        writer.Write(item.CountProducts);
                        writer.Write(item.PriceOneProducts);
                        writer.Write(item.SectionProducts);
                        writer.Write(item.ShirtProducts);
                    }
                }
            }
        }
    }
}
