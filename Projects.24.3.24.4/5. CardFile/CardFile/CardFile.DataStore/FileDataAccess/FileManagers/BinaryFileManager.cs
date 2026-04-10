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
                        newCard.Title = reader.ReadString();
                        newCard.Studio = reader.ReadString();
                        newCard.Genre = reader.ReadString();
                        newCard.ReleaseDate = new DateTime(reader.ReadInt64());
                        newCard.Platform = reader.ReadString();
                        newCard.Publisher = reader.ReadString();
                        newCard.PurchaseDate = new DateTime(reader.ReadInt64());

                        var isNullableDatePresent = reader.ReadBoolean();
                        if (isNullableDatePresent)
                            newCard.CompletionDate = new DateTime(reader.ReadInt64());
                        else
                            newCard.CompletionDate = null;

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
                    writer.Write(item.Title);
                    writer.Write(item.Studio);
                    writer.Write(item.Genre);
                    writer.Write(item.ReleaseDate.Ticks);
                    writer.Write(item.Platform);
                    writer.Write(item.Publisher);
                    writer.Write(item.PurchaseDate.Ticks);
                    writer.Write(item.CompletionDate.HasValue);
                    if (item.CompletionDate.HasValue)
                        writer.Write(item.CompletionDate.Value.Ticks);
                    writer.Write(item.Price);
                }
            }
        }
    }
}
