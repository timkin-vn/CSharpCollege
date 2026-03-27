using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using(var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using(var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if(string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if (lineParts.Length != 10)
                        {
                            throw new InvalidOperationException($"В строке файла {fileName} неверное количество полей");
                        }
                        var card = new CardDto();
                        if(!int.TryParse(lineParts[0], out var id))
                        {
                            throw new FormatException("Неверный формат");
                        }
                        else
                        {
                            card.Id = id;
                        }
                        card.FirstName = lineParts[1];
                        card.MiddleName = lineParts[2];
                        card.LastName = lineParts[3];
                        if (!DateTime.TryParse(lineParts[4], out var birthDate))
                        {
                            throw new FormatException("Неверный формат");
                        }
                        else
                        {
                            card.BirthDate = birthDate;
                        }
                        card.Department = lineParts[5];
                        card.Position = lineParts[6];
                        if (!DateTime.TryParse(lineParts[7], out var emDate))
                        {
                            throw new FormatException("Неверный формат");
                        }
                        else
                        {
                            card.EmploymentDate = emDate;
                        }
                        if (lineParts[8] == "-") card.DismissalDate = null;
                        else
                        {
                            if (!DateTime.TryParse(lineParts[8], out var desDate))
                            {
                                throw new FormatException("Неверный формат");
                            }
                            else
                            {
                                card.DismissalDate = desDate;
                            }
                            
                        }
                        if(!decimal.TryParse(lineParts[9], out decimal dec))
                        {
                            throw new FormatException("Неверный формат");
                        }
                        else
                        {
                            card.Salary = dec;
                        }
                        records.Add(card);
                    }
                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                            $"{item.BirthDate.ToShortDateString()};{item.Department};{item.Position};" +
                            $"{item.EmploymentDate.ToShortDateString()};{item.DismissalDate?.ToShortDateString() ?? "-"};" +
                            $"{item.Salary}");
                    }
                }
            }
        }
    }
}
