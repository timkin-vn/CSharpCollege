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
                            newRecord.MovieName = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newRecord.DateReles = new DateTime(ticks); 
                            var ticks_time = reader.ReadInt64();
                            newRecord.TimeGoes = new TimeSpan(ticks_time);

                            newRecord.PriseOneTickets = reader.ReadDecimal();
                            newRecord.CountTickets = reader.ReadInt32(); 
                            newRecord.LinePlace = reader.ReadInt16();
                            newRecord.Place = reader.ReadInt16();
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
                        writer.Write(item.MovieName);
                        writer.Write(item.MovieType);
                        writer.Write(item.DateReles.Ticks);
                        writer.Write(item.TimeGoes.Ticks);
                        writer.Write(item.PriseOneTickets);
                        writer.Write(item.CountTickets);
                        writer.Write(item.LinePlace);
                        writer.Write(item.Place);
                    }
                }
            }
        }
    }
}
