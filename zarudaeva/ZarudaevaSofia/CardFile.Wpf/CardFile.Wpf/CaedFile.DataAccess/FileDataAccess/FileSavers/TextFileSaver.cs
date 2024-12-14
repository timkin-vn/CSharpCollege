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

                        newRecord.MovieName = lineSegments[1];
                        newRecord.MovieType = lineSegments[2];
                       

                        if (DateTime.TryParse(lineSegments[3], out var dateReles))
                        {
                            newRecord.DateReles = dateReles;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        } 
                        if (TimeSpan.TryParse(lineSegments[4], out var timeGoes))
                        {
                            newRecord.TimeGoes = timeGoes;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (decimal.TryParse(lineSegments[5], out var priseOneTickets))
                        {
                            newRecord.PriseOneTickets = priseOneTickets;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }

                        if (int.TryParse(lineSegments[6], out var countTickets))
                        {
                            newRecord.CountTickets = countTickets;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        }
                        if (short.TryParse(lineSegments[7], out var linePlace))
                        {
                            newRecord.LinePlace = linePlace;
                        }
                        else
                        {
                            throw new Exception($"Неверный формат файла {fileName}");
                        } 
                        if (short.TryParse(lineSegments[8], out var place))
                        {
                            newRecord.Place = place;
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
                        writer.WriteLine($"{item.Id};{item.MovieName};{item.MovieType};{item.DateReles.ToShortDateString()};" +
                            $"{item.TimeGoes.ToString()};{item.PriseOneTickets};{item.CountTickets};{item.LinePlace};{item.Place}");
                    }
                }
            }
        }
    }
}
