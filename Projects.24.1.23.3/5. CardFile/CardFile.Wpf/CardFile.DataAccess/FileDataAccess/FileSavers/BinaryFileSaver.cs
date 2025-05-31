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
            {
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

                            var ticks = reader.ReadInt64();
                            newCard.BirthDate = new DateTime(ticks);

                            newCard.Department = reader.ReadString();
                            newCard.Position = reader.ReadString();

                            ticks = reader.ReadInt64();
                            newCard.EmploymentDate = new DateTime(ticks);

                            var isDismissalDatePresent = reader.ReadBoolean();
                            if (isDismissalDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newCard.DismissalDate = new DateTime(ticks);
                            }
                            else
                            {
                                newCard.DismissalDate = null;
                            }

                            newCard.Salary = reader.ReadDecimal();

                            records.Add(newCard);
                        }

                        collection.ReplaceAll(records);
                    }
                    catch
                    {
                        throw new Exception($"Неверный данные в файле {fileName}");
                    }
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
                        if (item.DismissalDate.HasValue)
                        {
                            writer.Write(item.DismissalDate.Value.Ticks);
                        }

                        writer.Write(item.Salary);
                    }
                }
            }
        }
    }
}
