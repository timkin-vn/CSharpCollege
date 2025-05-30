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
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<CardDto>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if (lineParts.Length != 10)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество полей");
                        }

                        var newCard = new CardDto();

                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newCard.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Id");
                        }

                        newCard.FirstName = lineParts[1];
                        newCard.MiddleName = lineParts[2];
                        newCard.LastName = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var birthDate))
                        {
                            newCard.BirthDate = birthDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение BirthDate");
                        }

                        newCard.Department = lineParts[5];
                        newCard.Position = lineParts[6];

                        if (DateTime.TryParse(lineParts[7], out var employmentDate))
                        {
                            newCard.EmploymentDate = employmentDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение EmploymentDate");
                        }

                        if (lineParts[8] == "-")
                        {
                            newCard.DismissalDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[8], out var dismissalDate))
                        {
                            newCard.DismissalDate = dismissalDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DismissalDate");
                        }

                        if (decimal.TryParse(lineParts[9], out var salary))
                        {
                            newCard.Salary = salary;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Salary");
                        }

                        records.Add(newCard);
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
