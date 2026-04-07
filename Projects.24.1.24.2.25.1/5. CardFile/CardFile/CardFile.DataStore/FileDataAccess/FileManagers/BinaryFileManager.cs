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

                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newCard = new CardDto
                        {
                            Id = reader.ReadInt32(),
                            Nickname = reader.ReadString(),
                            RealName = reader.ReadString(),
                            Country = reader.ReadString(),
                            BirthDate = new DateTime(reader.ReadInt64()),
                            Team = reader.ReadString(),
                            Role = reader.ReadString(),
                            TotalEarnings = reader.ReadDecimal(),
                            Achievements = reader.ReadString()
                        };
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
                    writer.Write(item.Nickname ?? "");
                    writer.Write(item.RealName ?? "");
                    writer.Write(item.Country ?? "");
                    writer.Write(item.BirthDate.Ticks);
                    writer.Write(item.Team ?? "");
                    writer.Write(item.Role ?? "");
                    writer.Write(item.TotalEarnings);
                    writer.Write(item.Achievements ?? "");
                }
            }
        }
    }
}