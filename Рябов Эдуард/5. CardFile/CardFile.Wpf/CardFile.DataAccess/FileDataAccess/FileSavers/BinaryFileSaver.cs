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
                            newCard.Type = reader.ReadString();
                            newCard.Manufacturer = reader.ReadString();
                            newCard.Price = reader.ReadDecimal();
                            newCard.StockQuantity = reader.Read();
                            newCard.NameBay = reader.ReadString();
                            newCard.PhoneNumber = reader.Read();
                            newCard.QuantitySell = reader.Read();



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
                        writer.Write(item.Type);
                        writer.Write(item.Manufacturer);
                        writer.Write(item.Price);
                        writer.Write(item.StockQuantity);
                        writer.Write(item.NameBay);
                        writer.Write(item.PhoneNumber);
                        writer.Write(item.QuantitySell);
                       
                    }
                }
            }
        }
    }
}
