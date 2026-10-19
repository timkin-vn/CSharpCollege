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
                            newCard.Category = reader.ReadString();
                            newCard.Brand = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newCard.DateAdded = new DateTime(ticks);

                            newCard.Supplier = reader.ReadString();
                            newCard.StorageLocation = reader.ReadString();

                            ticks = reader.ReadInt64();
                            newCard.LastRestockDate = new DateTime(ticks);

                            var isDismissalDatePresent = reader.ReadBoolean();
                            if (isDismissalDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newCard.ExpirationDate = new DateTime(ticks);
                            }
                            else
                            {
                                newCard.ExpirationDate = null;
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
                        writer.Write(item.ProductName);
                        writer.Write(item.Category);
                        writer.Write(item.Brand);
                        writer.Write(item.DateAdded.Ticks);
                        writer.Write(item.Supplier);
                        writer.Write(item.StorageLocation);
                        writer.Write(item.LastRestockDate.Ticks);

                        writer.Write(item.ExpirationDate.HasValue);
                        if (item.ExpirationDate.HasValue)
                        {
                            writer.Write(item.ExpirationDate.Value.Ticks);
                        }

                        writer.Write(item.Price);
                    }
                }
            }    
        }
    }
}
