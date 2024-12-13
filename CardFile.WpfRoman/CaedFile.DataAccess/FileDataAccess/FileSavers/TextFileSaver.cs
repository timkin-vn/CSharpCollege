﻿using CardFile.DataAccess.DataCollection;
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
        public void Open(string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var inputLine = reader.ReadLine();
                        if (string.IsNullOrEmpty(inputLine))
                        {
                            continue;
                        }

                        var lineSegments = inputLine.Split(';');
                        if (lineSegments.Length != 10)
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        var newRecord = new CardDto();
                        if (int.TryParse(lineSegments[0], out var id))
                        {
                            newRecord.Id = id;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        newRecord.FirstName = lineSegments[1];
                        newRecord.MiddleName = lineSegments[2];
                        newRecord.LastName = lineSegments[3]; 
                        newRecord.NameClass = lineSegments[7];

                        if (DateTime.TryParse(lineSegments[4], out var birthDate))
                        {
                            newRecord.BirthDate = birthDate;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (decimal.TryParse(lineSegments[5], out var paymentAmount))
                        {
                            newRecord.PaymentAmount = paymentAmount;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (int.TryParse(lineSegments[6], out var childrenCount))
                        {
                            newRecord.ChildrenCountClass = childrenCount;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        } 
                        if (int.TryParse(lineSegments[8], out var numberClass))
                        {
                            newRecord.NumberClass = numberClass;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        } 
                        if (bool.TryParse(lineSegments[9], out var correct))
                        {
                            newRecord.Correctional_class = correct;
                        }
                        else
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
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.FirstName};{item.MiddleName};{item.LastName};" +
                            $"{item.BirthDate.ToShortDateString()};{item.PaymentAmount};{item.ChildrenCountClass};" +
                            $"{item.NameClass};" +
                            $"{item.NumberClass};{item.Correctional_class}");
                    }
                }
            }
        }
    }
}
