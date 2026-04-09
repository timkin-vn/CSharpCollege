using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

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
                            newCard.Title = reader.ReadString();
                            newCard.Text = reader.ReadString();
                            newCard.Category = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newCard.CreatedAt = new DateTime(ticks);

                            newCard.IsDone = reader.ReadBoolean();
                            newCard.IsPinned = reader.ReadBoolean();

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
                        writer.Write(item.Title ?? string.Empty);
                        writer.Write(item.Text ?? string.Empty);
                        writer.Write(item.Category ?? string.Empty);
                        writer.Write(item.CreatedAt.Ticks);
                        writer.Write(item.IsDone);
                        writer.Write(item.IsPinned);
                    }
                }
            }
        }
    }
}