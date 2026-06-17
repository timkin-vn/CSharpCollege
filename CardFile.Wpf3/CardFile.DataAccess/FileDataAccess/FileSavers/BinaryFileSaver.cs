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
        public void OpenFile(string fileName, CardCollection collection)
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
                            var newRecord = new CardDto();

                            newRecord.Id = reader.ReadInt32();
                            newRecord.BookName = reader.ReadString();
                            newRecord.AuthorFirstName = reader.ReadString();
                            newRecord.AuthorLastName = reader.ReadString();
                            newRecord.Genre = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.DateOfPublishing = new DateTime(ticks);

                            newRecord.Edition = reader.ReadString();
                            newRecord.Price = reader.ReadInt32();
                            newRecord.AmountOfPages = reader.ReadInt32();

                            var isDateOfDelistingPresent = reader.ReadBoolean();
                            if (isDateOfDelistingPresent)
                            {
                                ticks = reader.ReadInt64();
                                newRecord.DateOfDelisting = new DateTime(ticks);
                            }

                            newRecord.Rating = reader.ReadDecimal();

                            records.Add(newRecord);
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

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.BookName);
                        writer.Write(item.AuthorFirstName);
                        writer.Write(item.AuthorLastName);
                        writer.Write(item.Genre);
                        writer.Write(item.DateOfPublishing.Ticks);
                        writer.Write(item.Edition);
                        writer.Write(item.Price);
                        writer.Write(item.AmountOfPages);
                        writer.Write(item.DateOfDelisting.HasValue);

                        if (item.DateOfDelisting.HasValue)
                        {
                            writer.Write(item.DateOfDelisting.Value.Ticks);
                        }

                        writer.Write(item.Rating);
                    }
                }
            }
        }
    }
}
