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
                            var newCard = new CardDto();

                            newCard.Id = reader.ReadInt32();
                            newCard.Name = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newCard.YearOfProduction = new DateTime(ticks);

                            newCard.Type = reader.ReadString();

                            ticks = reader.ReadInt64();
                            newCard.DeliveryDate = new DateTime(ticks);

                            newCard.Price = reader.ReadInt32();
                            records.Add(newCard);
                        }

                        collection.ReplaceAll(records);
                    }
                    catch
                    {
                        throw new Exception($"Неверный данные в файле {fileName}");
                    }
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
                        writer.Write(item.Name);
                        writer.Write(item.YearOfProduction.ToString());
                        writer.Write(item.Type);
                        writer.Write(item.DeliveryDate.ToString());
                        writer.Write(item.Price);
                    }
                }
            }
        }
    }
}
