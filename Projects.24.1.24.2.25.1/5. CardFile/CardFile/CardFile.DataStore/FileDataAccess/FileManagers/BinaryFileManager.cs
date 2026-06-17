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
                        var newMovie = new CardDto
                        {
                            Id = reader.ReadInt32(),
                            Title = reader.ReadString(),
                            Director = reader.ReadString(),
                            Year = reader.ReadInt32(),
                            Genre = reader.ReadString(),
                            Duration = reader.ReadInt32(),
                            Rating = reader.ReadDecimal()
                        };
                        records.Add(newMovie);
                    }
                }
                catch
                {
                    throw new Exception($"Неверные данные в бинарном файле {fileName}");
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
                    writer.Write(item.Director);
                    writer.Write(item.Year);
                    writer.Write(item.Genre);
                    writer.Write(item.Duration);
                    writer.Write(item.Rating);
                }
            }
        }
    }
}
