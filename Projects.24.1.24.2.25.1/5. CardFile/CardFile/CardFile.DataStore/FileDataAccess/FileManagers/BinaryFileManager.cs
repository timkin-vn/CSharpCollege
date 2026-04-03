using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
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
                            newCard.ProductName = reader.ReadString();
                            newCard.ProductModel = reader.ReadString();
                            newCard.ProductColor = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newCard.ManufactureDate = new DateTime(ticks);

                            newCard.Category = reader.ReadString();
                            newCard.Manufacturer = reader.ReadString();

                            ticks = reader.ReadInt64();
                            newCard.ReceiptDate = new DateTime(ticks);

                            var isWriteOffPresent = reader.ReadBoolean();
                            if (isWriteOffPresent)
                            {
                                ticks = reader.ReadInt64();
                                newCard.WriteOffDate = new DateTime(ticks);
                            }
                            else
                            {
                                newCard.WriteOffDate = null;
                            }

                            newCard.Price = reader.ReadDecimal();

                            records.Add(newCard);
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
                        writer.Write(item.ProductName ?? "");
                        writer.Write(item.ProductModel ?? "");
                        writer.Write(item.ProductColor ?? "");
                        writer.Write(item.ManufactureDate.Ticks);
                        writer.Write(item.Category ?? "");
                        writer.Write(item.Manufacturer ?? "");
                        writer.Write(item.ReceiptDate.Ticks);

                        writer.Write(item.WriteOffDate.HasValue);
                        if (item.WriteOffDate.HasValue)
                        {
                            writer.Write(item.WriteOffDate.Value.Ticks);
                        }

                        writer.Write(item.Price);
                    }
                }
            }
        }
    }
}