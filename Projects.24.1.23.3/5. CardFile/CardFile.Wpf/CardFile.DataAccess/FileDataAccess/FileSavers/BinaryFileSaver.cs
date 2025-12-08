using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class BinaryFileSaver : IFileSaver
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
                        newCard.Year = reader.ReadInt32();
                        newCard.Genre = reader.ReadString();
                        newCard.Description = reader.ReadString();

                        records.Add(newCard);
                    }

                    collection.ReplaceAll(records);
                }
                catch
                {
                    throw new Exception($"Неверные данные в файле {fileName}");
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
                        writer.Write(item.Title);
                        writer.Write(item.Author);
                        writer.Write(item.Year);
                        writer.Write(item.Genre);
                        writer.Write(item.Description);
                    }
                }
            }
        }
    }
}
