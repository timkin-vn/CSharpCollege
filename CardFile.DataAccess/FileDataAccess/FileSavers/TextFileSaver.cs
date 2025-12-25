using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.FileSavers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CardFile.Business.Services.FileSavers
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
                        if (lineParts.Length != 12)
                        {
                            throw new Exception($"В строке файла {fileName} неверное кол-во записей");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение ID");
                        }

                        newRecord.FirstName = lineParts[1];
                        newRecord.MiddleName = lineParts[2];
                        newRecord.LastName = lineParts[3];

                        if (DateTime.TryParse(lineParts[4], out var birthdate))
                        {
                            newRecord.BirthDate = birthdate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение BirthDate");
                        }


                        if (DateTime.TryParse(lineParts[5], out var registrationdate))
                        {
                            newRecord.RegistrationDate = registrationdate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение RegistrationDate");
                        }

                        newRecord.Autor = lineParts[6];
                        newRecord.Genre = lineParts[7];
                        newRecord.Book = lineParts[8];

                        if (DateTime.TryParse(lineParts[9], out var getdate))
                        {
                            newRecord.GetDate = getdate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение GetDate");
                        }

                        if (lineParts[10] == "-")
                        {
                            newRecord.RefundDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[10], out var refunddate))
                        {
                            newRecord.RefundDate = refunddate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение RefundDate");
                        }

                        if (decimal.TryParse(lineParts[11], out var col))
                        {
                            newRecord.Collection = col;
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
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};{item.BirthDate.ToShortDateString()};" +
                            $"{item.RegistrationDate.ToShortDateString()};{item.Autor};{item.Genre};{item.Book};{item.GetDate.ToShortDateString()};" +
                            $"{item.RefundDate?.ToShortDateString() ?? "-"};{item.Collection}");
                    }
                }
            }
        }
    }
}
