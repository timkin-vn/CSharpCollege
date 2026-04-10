using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, BookCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<BookDto>();

                    try
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var newBook = new BookDto();

                            newBook.Id = reader.ReadInt32();
                            newBook.Title = reader.ReadString();
                            newBook.Author = reader.ReadString();
                            newBook.Genre = reader.ReadString();
                            newBook.Year = reader.ReadInt32();
                            newBook.Copies = reader.ReadInt32();

                            var ticks = reader.ReadInt64();
                            newBook.AddedDate = new DateTime(ticks);

                            var hasDeletedDate = reader.ReadBoolean();
                            if (hasDeletedDate)
                            {
                                ticks = reader.ReadInt64();
                                newBook.DeletedDate = new DateTime(ticks);
                            }
                            else
                            {
                                newBook.DeletedDate = null;
                            }

                            records.Add(newBook);
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

        public void SaveToFile(string fileName, BookCollection collection)
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
                        writer.Write(item.Genre);
                        writer.Write(item.Year);
                        writer.Write(item.Copies);
                        writer.Write(item.AddedDate.Ticks);

                        writer.Write(item.DeletedDate.HasValue);
                        if (item.DeletedDate.HasValue)
                        {
                            writer.Write(item.DeletedDate.Value.Ticks);
                        }
                    }
                }
            }
        }
    }
}