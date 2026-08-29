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
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            using(var fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                using(var reader =  new StreamReader(fs))
                {
                    var records = new List<LimerDto>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if(!string.IsNullOrWhiteSpace(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if(lineParts.Length != 6)
                        {
                            throw new InvalidOperationException($"В строке файла {filePath} неверное количество полей");
                        }
                        var limer = new LimerDto();
                        if(int.TryParse(lineParts[0], out var id))
                        {
                            limer.Id = id;
                        }
                        else
                        {
                            throw new FormatException("Введен неверный формат поля ID");
                        }
                        limer.FirstName = lineParts[1];
                        limer.MiddleName = lineParts[2];
                        limer.LastName = lineParts[3];
                        if (DateTime.TryParse(lineParts[4], out var date))
                        {
                            limer.BirthDate = date;
                        }
                        else
                        {
                            throw new FormatException("Введен неверный формат поля BirthDate");
                        }
                        limer.Group = lineParts[5];
                        records.Add(limer);
                    }
                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};{item.BirthDate.ToShortDateString()};{item.Group}");
                    }
                }
            }
        }
    }
}
