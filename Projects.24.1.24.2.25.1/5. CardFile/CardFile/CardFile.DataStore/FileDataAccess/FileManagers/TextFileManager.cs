using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
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
                        if (lineParts.Length != 9)
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

                        newCard.Name = lineParts[1];
                        newCard.LoreName = lineParts[2];
                        newCard.PrimaryAttribute = lineParts[3];
                        newCard.AttackType = lineParts[4];
                        newCard.Role = lineParts[5];

                        if (DateTime.TryParse(lineParts[6], out var releaseDate))
                        {
                            newCard.ReleaseDate = releaseDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение ReleaseDate");
                        }

                        if (lineParts[7] == "-")
                        {
                            newCard.ReworkDate = null;
                        }
                        else if (DateTime.TryParse(lineParts[7], out var reworkDate))
                        {
                            newCard.ReworkDate = reworkDate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение ReworkDate");
                        }

                        if (decimal.TryParse(lineParts[8], out var winRate))
                        {
                            newCard.WinRate = winRate;
                        }
                        else
                        {
                            throw new Exception($"В строке файла {fileName} неверное значение WinRate");
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
                        writer.WriteLine($"{item.Id};{item.Name};{item.LoreName};{item.PrimaryAttribute};" +
                            $"{item.AttackType};{item.Role};{item.ReleaseDate.ToShortDateString()};" +
                            $"{item.ReworkDate?.ToShortDateString() ?? "-"};{item.WinRate}");
                    }
                }
            }
        }
    }
}
