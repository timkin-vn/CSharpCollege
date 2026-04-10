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
                        newCard.Author = reader.ReadString();
                        newCard.Genre = reader.ReadString();
                        newCard.PublicationDate = new DateTime(reader.ReadInt64());
                        newCard.Publisher = reader.ReadString();
                        newCard.Language = reader.ReadString();
                        newCard.ArrivalDate = new DateTime(reader.ReadInt64());

                        var isNullableDatePresent = reader.ReadBoolean();
                        if (isNullableDatePresent)
                            newCard.BorrowedUntil = new DateTime(reader.ReadInt64());
                        else
                            newCard.BorrowedUntil = null;

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
                    writer.Write(item.Author);
                    writer.Write(item.Genre);
                    writer.Write(item.PublicationDate.Ticks);
                    writer.Write(item.Publisher);
                    writer.Write(item.Language);
                    writer.Write(item.ArrivalDate.Ticks);
                    writer.Write(item.BorrowedUntil.HasValue);
                    if (item.BorrowedUntil.HasValue)
                        writer.Write(item.BorrowedUntil.Value.Ticks);
                    writer.Write(item.Price);
                }
            }
        }
    }
}
