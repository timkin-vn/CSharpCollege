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
    internal class TextFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();

                    while (!reader.EndOfStream)
                    {
                        var nextLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(nextLine))
                        {
                            continue;
                        }

                        var lineParts = nextLine.Split(';');
                        if (lineParts.Length != 10)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество записей");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Id");
                        }

                        newRecord.FirstName = lineParts[1];
                        newRecord.MiddleName = lineParts[2];
                        newRecord.LastName = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var birthDate))
                        {
                            newRecord.BirthDate = birthDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение BirthDate");
                        }

                        newRecord.Department = lineParts[5];
                        newRecord.Position = lineParts[6];

                        if (DateTime.TryParse(lineParts[7], out var employmentDate))
                        {
                            newRecord.EmploymentDate = employmentDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение EmploymentDate");
                        }

                        if (lineParts[8] == "-")
                        {
                            newRecord.DismissalDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[8], out var dismissalDate))
                        {
                            newRecord.DismissalDate = dismissalDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DismissalDate");
                        }

                        if (decimal.TryParse(lineParts[9], out var salary))
                        {
                            newRecord.Salary = salary;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Salary");
                        }

                        records.Add(newRecord);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
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
