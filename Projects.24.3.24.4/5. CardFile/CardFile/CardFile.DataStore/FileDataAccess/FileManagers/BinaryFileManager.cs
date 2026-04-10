using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                var records = new List<CardDto>();

                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newCard = new CardDto();
                        newCard.Id = reader.ReadInt32();
                        newCard.ClientFirstName = reader.ReadString();
                        newCard.ClientLastName = reader.ReadString();
                        newCard.ProductName = reader.ReadString();
                        newCard.OrderDate = new DateTime(reader.ReadInt64());
                        newCard.Address = reader.ReadString();
                        newCard.DeliveryMethod = reader.ReadString();
                        newCard.ShippingDate = new DateTime(reader.ReadInt64());

                        var isNullableDatePresent = reader.ReadBoolean();
                        if (isNullableDatePresent)
                            newCard.ReceivedDate = new DateTime(reader.ReadInt64());
                        else
                            newCard.ReceivedDate = null;

                        newCard.TotalAmount = reader.ReadDecimal();
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

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.ClientFirstName);
                    writer.Write(item.ClientLastName);
                    writer.Write(item.ProductName);
                    writer.Write(item.OrderDate.Ticks);
                    writer.Write(item.Address);
                    writer.Write(item.DeliveryMethod);
                    writer.Write(item.ShippingDate.Ticks);
                    writer.Write(item.ReceivedDate.HasValue);
                    if (item.ReceivedDate.HasValue)
                        writer.Write(item.ReceivedDate.Value.Ticks);
                    writer.Write(item.TotalAmount);
                }
            }
        }
    }
}
