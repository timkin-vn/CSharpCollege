using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<LimerDto>();
                    try
                    {
                        var limer = new LimerDto();
                        limer.Id = reader.ReadInt32();
                        limer.FirstName = reader.ReadString();
                        limer.MiddleName = reader.ReadString();
                        limer.LastName = reader.ReadString();
                        var ticks = reader.ReadInt64();
                        limer.BirthDate = new DateTime(ticks);
                        limer.Group = reader.ReadString();
                        records.Add(limer);
                    }
                    catch
                    {
                        throw new Exception($"Неверные данные в файле {filePath}");
                    }
                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
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
                        writer.Write(item.Group);
                    }
                }
            }
        }
    }
}
