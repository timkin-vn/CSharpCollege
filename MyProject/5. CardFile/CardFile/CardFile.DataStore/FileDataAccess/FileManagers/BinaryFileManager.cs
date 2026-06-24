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
                        var newCard = new CardDto();

                        newCard.Id = reader.ReadInt32();
                        newCard.FirstName = reader.ReadString();
                        newCard.MiddleName = reader.ReadString();
                        newCard.LastName = reader.ReadString();
                        newCard.BirthDate = new DateTime(reader.ReadInt64());
                        newCard.DeathDate = new DateTime(reader.ReadInt64());
                        newCard.CauseOfDeath = reader.ReadString();
                        newCard.PlaceOfDeath = reader.ReadString();
                        newCard.AdmissionDate = new DateTime(reader.ReadInt64());
                        newCard.SectionNumber = reader.ReadString();
                        newCard.IsReleased = reader.ReadBoolean();

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
                    writer.Write(item.FirstName ?? "");
                    writer.Write(item.MiddleName ?? "");
                    writer.Write(item.LastName ?? "");
                    writer.Write(item.BirthDate.Ticks);
                    writer.Write(item.DeathDate.Ticks);
                    writer.Write(item.CauseOfDeath ?? "");
                    writer.Write(item.PlaceOfDeath ?? "");
                    writer.Write(item.AdmissionDate.Ticks);
                    writer.Write(item.SectionNumber ?? "");
                    writer.Write(item.IsReleased);
                }
            }
        }
    }
}