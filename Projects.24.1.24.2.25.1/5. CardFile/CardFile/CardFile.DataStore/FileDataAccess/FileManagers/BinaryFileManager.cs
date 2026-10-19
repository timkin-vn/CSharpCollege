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
                        newCard.Name = reader.ReadString();
                        newCard.Industry = reader.ReadString();
                        newCard.FoundationYear = reader.ReadInt32();
                        newCard.Revenue = reader.ReadDecimal();
                        newCard.Employees = reader.ReadInt32();
                        newCard.Description = reader.ReadString();

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
                    writer.Write(item.Name ?? string.Empty);
                    writer.Write(item.Industry ?? string.Empty);
                    writer.Write(item.FoundationYear);
                    writer.Write(item.Revenue);
                    writer.Write(item.Employees);
                    writer.Write(item.Description ?? string.Empty);
                }
            }
        }
    }
}