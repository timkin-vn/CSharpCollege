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
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var newRecord = new CardDto();

                        try
                        {
                            newRecord.Id = reader.ReadInt32();
                            newRecord.Bank_name = reader.ReadString();
                            newRecord.ATM_count = reader.ReadInt32();
                            newRecord.Street = reader.ReadString();
                            newRecord.House = reader.ReadString();
                            newRecord.City = reader.ReadString();
                            newRecord.Money_count = reader.ReadDecimal();
                            newRecord.Money_limit = reader.ReadInt32();
                            newRecord.Card_number = reader.ReadString();

                        }
                        catch
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        records.Add(newRecord);
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.Bank_name);
                        writer.Write(item.ATM_count);
                        writer.Write(item.Street);
                        writer.Write(item.House);
                        writer.Write(item.City);
                        writer.Write(item.Money_limit);
                        writer.Write(item.Money_limit);

                        writer.Write(item.Card_number[0]);
                        writer.Write(item.Card_number[1]);
                        writer.Write(item.Card_number[2]);
                        writer.Write(item.Card_number[3]);
                    }
                }
            }
        }
    }
}
