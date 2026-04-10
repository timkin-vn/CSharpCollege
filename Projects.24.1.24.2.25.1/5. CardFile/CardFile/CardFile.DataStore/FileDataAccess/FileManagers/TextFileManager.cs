using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, BookCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var records = new List<BookDto>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if (lineParts.Length != 8)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество полей");
                        }

                        var newBook = new BookDto();

                        if (int.TryParse(lineParts[0], out var id))
                        {
                            newBook.Id = id;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Id");
                        }

                        newBook.Title = lineParts[1];
                        newBook.Author = lineParts[2];
                        newBook.Genre = lineParts[3];

                        if (int.TryParse(lineParts[4], out var year))
                        {
                            newBook.Year = year;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Year");
                        }

                        if (int.TryParse(lineParts[5], out var copies))
                        {
                            newBook.Copies = copies;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение Copies");
                        }

                        if (DateTime.TryParse(lineParts[6], out var addedDate))
                        {
                            newBook.AddedDate = addedDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение AddedDate");
                        }

                        if (lineParts[7] == "-")
                        {
                            newBook.DeletedDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[7], out var deletedDate))
                        {
                            newBook.DeletedDate = deletedDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение DeletedDate");
                        }

                        records.Add(newBook);
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, BookCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.Title};{item.Author};{item.Genre};" +
                            $"{item.Year};{item.Copies};{item.AddedDate.ToShortDateString()};" +
                            $"{item.DeletedDate?.ToShortDateString() ?? "-"}");
                    }
                }
            }
        }
    }
}