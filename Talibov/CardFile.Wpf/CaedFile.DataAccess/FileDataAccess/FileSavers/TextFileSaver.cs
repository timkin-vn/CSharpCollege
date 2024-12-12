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
                        if (lineSegments.Length != 7)
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

                        newRecord.Name = lineSegments[1];
                        newRecord.Description = lineSegments[2];
                        newRecord.Street = lineSegments[3];
                        newRecord.House = lineSegments[4];
                        newRecord.City = lineSegments[5];

                        if (int.TryParse(lineSegments[6], out var mailIndex))
                        {
                            newRecord.MailIndex = mailIndex;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (double.TryParse(lineSegments[7], out var rating))
                        {
                            newRecord.Rating = rating;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (int.TryParse(lineSegments[8], out var counterReviews))
                        {
                            newRecord.CounterReviews = counterReviews;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }
                        newRecord.Status = lineSegments[9];
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
                        writer.WriteLine($"{item.Id};{item.Name};{item.Description};{item.Street};" +
                            $"{item.House};{item.City};{item.MailIndex};{item.Rating};{item.CounterReviews};{item.Status}");
                    }
                }
            }
        }
    }
}
