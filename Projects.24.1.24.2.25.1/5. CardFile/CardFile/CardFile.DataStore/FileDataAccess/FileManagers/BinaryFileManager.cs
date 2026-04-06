using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
    {
        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                var items = collection.GetAll().ToList();

                // Сначала записываем NextId, чтобы восстановить его при открытии
                writer.Write(collection.NextId);
                // Записываем количество записей
                writer.Write(items.Count);

                foreach (var item in items)
                {
                    writer.Write(item.Id);
                    writer.Write(item.Title ?? "");
                    writer.Write(item.Genre ?? "");
                    writer.Write(item.GlobalRating);
                    writer.Write(item.MyScore);
                    writer.Write(item.ReleaseDate.Ticks);
                    writer.Write(item.Description ?? "");
                }
            }
        }
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            if (!File.Exists(fileName)) return;

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                try
                {
                    int nextId = reader.ReadInt32();
                    int count = reader.ReadInt32();
                    var records = new List<CardDto>();

                    for (int i = 0; i < count; i++)
                    {
                        records.Add(new CardDto
                        {
                            Id = reader.ReadInt32(),
                            Title = reader.ReadString(),
                            Genre = reader.ReadString(),
                            GlobalRating = reader.ReadDouble(),
                            MyScore = reader.ReadInt32(),
                            ReleaseDate = new DateTime(reader.ReadInt64()),
                            Description = reader.ReadString()
                        });
                    }
                    collection.ReplaceAll(records, nextId);
                }
                catch
                {
                    throw new Exception($"Ошибка при чтении бинарного файла {fileName}");
                }
            }
        }
    }
}

