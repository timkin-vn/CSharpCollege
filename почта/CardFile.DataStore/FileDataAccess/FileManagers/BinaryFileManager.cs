using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(fs))
            {
                var records = new List<LetterDto>();
                try
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var l = new LetterDto();
                        l.Id = reader.ReadInt32();
                        l.Sender = reader.ReadString();
                        l.Receiver = reader.ReadString();
                        l.Subject = reader.ReadString();
                        l.Body = reader.ReadString();
                        l.Date = new DateTime(reader.ReadInt64());
                        l.IsRead = reader.ReadBoolean();
                        records.Add(l);
                    }
                }
                catch { throw new Exception($"Неверные данные в файле {fileName}"); }
                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.Write(item.Id);
                    writer.Write(item.Sender);
                    writer.Write(item.Receiver);
                    writer.Write(item.Subject);
                    writer.Write(item.Body);
                    writer.Write(item.Date.Ticks);
                    writer.Write(item.IsRead);
                }
            }
        }
    }
}