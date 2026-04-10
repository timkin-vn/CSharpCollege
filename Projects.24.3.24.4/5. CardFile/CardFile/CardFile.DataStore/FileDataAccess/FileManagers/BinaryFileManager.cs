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
                        newCard.Artist = reader.ReadString();
                        newCard.AlbumTitle = reader.ReadString();
                        newCard.Genre = reader.ReadString();
                        newCard.ReleaseDate = new DateTime(reader.ReadInt64());
                        newCard.Label = reader.ReadString();
                        newCard.Format = reader.ReadString();
                        newCard.PurchaseDate = new DateTime(reader.ReadInt64());

                        var isNullableDatePresent = reader.ReadBoolean();
                        if (isNullableDatePresent)
                            newCard.LastListenDate = new DateTime(reader.ReadInt64());
                        else
                            newCard.LastListenDate = null;

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
                    writer.Write(item.Artist);
                    writer.Write(item.AlbumTitle);
                    writer.Write(item.Genre);
                    writer.Write(item.ReleaseDate.Ticks);
                    writer.Write(item.Label);
                    writer.Write(item.Format);
                    writer.Write(item.PurchaseDate.Ticks);
                    writer.Write(item.LastListenDate.HasValue);
                    if (item.LastListenDate.HasValue)
                        writer.Write(item.LastListenDate.Value.Ticks);
                    writer.Write(item.Price);
                }
            }
        }
    }
}
