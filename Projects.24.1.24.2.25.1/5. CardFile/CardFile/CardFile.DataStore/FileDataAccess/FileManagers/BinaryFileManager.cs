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
            using (var reader = new BinaryReader(fs))
            {
                var records = new List<CardDto>();
                int nextId = 1;

                try
                {
                    if (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        nextId = reader.ReadInt32();
                    }

                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newCard = new CardDto
                        {
                            Id = reader.ReadInt32(),
                            Title = reader.ReadString(),
                            Director = reader.ReadString(),
                            ReleaseYear = reader.ReadInt32(),
                            Genre = reader.ReadString(),
                            DurationMinutes = reader.ReadDecimal(),
                            Rating = reader.ReadInt32(),
                            IsAvailable = reader.ReadBoolean()
                        };

                        records.Add(newCard);
                    }
                }
                catch
                {
                    throw new Exception($"Неверные данные в файле {fileName}");
                }

                collection.ReplaceAll(records, nextId);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                writer.Write(collection.NextId);

                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.Title ?? string.Empty);
                    writer.Write(item.Director ?? string.Empty);
                    writer.Write(item.ReleaseYear);
                    writer.Write(item.Genre ?? string.Empty);
                    writer.Write(item.DurationMinutes);
                    writer.Write(item.Rating);
                    writer.Write(item.IsAvailable);
                }
            }
        }
    }
}
