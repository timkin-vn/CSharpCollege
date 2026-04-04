using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using(var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using(var reader=new BinaryReader(fs))
                {
                    var records = new List<CardDto>();
                    try
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var card = new CardDto();
                            card.Id = reader.ReadInt32();
                            card.FirstName = reader.ReadString();
                            card.MiddleName = reader.ReadString();
                            card.LastName = reader.ReadString();
                            var ticks = reader.ReadInt64();
                            card.BirthDate = new DateTime(ticks);
                            card.Department = reader.ReadString();
                            card.Position = reader.ReadString();
                            ticks = reader.ReadInt64();
                            card.EmploymentDate = new DateTime(ticks);
                            var isDicimalDatePresent = reader.ReadBoolean();
                            if (isDicimalDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                card.DismissalDate = new DateTime(ticks);
                            }
                            else
                            {
                                card.DismissalDate = null;
                            }
                            card.Salary = reader.ReadDecimal();
                            card.Language = reader.ReadString();
                            records.Add(card);
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

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.FirstName);
                        writer.Write(item.MiddleName);
                        writer.Write(item.LastName);
                        writer.Write(item.BirthDate.Ticks);
                        writer.Write(item.Department);
                        writer.Write(item.Position);
                        writer.Write(item.EmploymentDate.Ticks);
                        writer.Write(item.DismissalDate.HasValue);
                        if(item.DismissalDate.HasValue)
                        {
                            writer.Write(item.DismissalDate.Value.Ticks);
                        }
                        writer.Write(item.Salary);
                        writer.Write(item.Language);
                    }
                }
            }
        }
    }
}
