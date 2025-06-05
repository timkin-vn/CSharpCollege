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
                            newRecord.DishName = reader.ReadString();
                            newRecord.Category = reader.ReadString();
                            newRecord.Description = reader.ReadString();
                            newRecord.PortionSize = reader.ReadInt32();
                            newRecord.Price = reader.ReadInt32();
                            newRecord.IsAvaliableNow = reader.ReadBoolean();
                            newRecord.SeasonDish = reader.ReadBoolean();
                            newRecord.IsVegan = reader.ReadBoolean();
                            newRecord.IsSpicy = reader.ReadBoolean();
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
                        writer.Write(item.DishName);
                        writer.Write(item.Category);
                        writer.Write(item.Description);
                        writer.Write(item.PortionSize);
                        writer.Write(item.Price);
                    }
                }
            }
        }
    }
}
