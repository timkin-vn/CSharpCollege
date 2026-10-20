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
                        newCard.Brand = reader.ReadString();
                        newCard.Model = reader.ReadString();
                        newCard.LicensePlate = reader.ReadString();
                        newCard.ManufactureDate = new DateTime(reader.ReadInt64());
                        newCard.Color = reader.ReadString();
                        newCard.Owner = reader.ReadString();
                        newCard.RegistrationDate = new DateTime(reader.ReadInt64());

                        var isNullableDatePresent = reader.ReadBoolean();
                        if (isNullableDatePresent)
                            newCard.DeregistrationDate = new DateTime(reader.ReadInt64());
                        else
                            newCard.DeregistrationDate = null;

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

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.Brand);
                    writer.Write(item.Model);
                    writer.Write(item.LicensePlate);
                    writer.Write(item.ManufactureDate.Ticks);
                    writer.Write(item.Color);
                    writer.Write(item.Owner);
                    writer.Write(item.RegistrationDate.Ticks);
                    writer.Write(item.DeregistrationDate.HasValue);
                    if (item.DeregistrationDate.HasValue)
                        writer.Write(item.DeregistrationDate.Value.Ticks);
                    writer.Write(item.Price);
                }
            }
        }
    }
}
