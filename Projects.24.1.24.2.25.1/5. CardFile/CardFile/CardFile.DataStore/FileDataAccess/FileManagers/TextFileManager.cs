using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    var heroes = new List<string>();
                    var items = new List<string>();
                    var neutrals = new List<string>();

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        if (line.StartsWith("@heroes;", StringComparison.OrdinalIgnoreCase))
                        {
                            heroes.AddRange(ParseCatalogLine(line, "@heroes;"));
                            continue;
                        }

                        if (line.StartsWith("@items;", StringComparison.OrdinalIgnoreCase))
                        {
                            items.AddRange(ParseCatalogLine(line, "@items;"));
                            continue;
                        }

                        if (line.StartsWith("@neutrals;", StringComparison.OrdinalIgnoreCase))
                        {
                            neutrals.AddRange(ParseCatalogLine(line, "@neutrals;"));
                            continue;
                        }

                        var lineParts = line.Split(';');
                        if (lineParts.Length != 9)
                        {
                            throw new Exception($"В строке файла {fileName} неверное количество полей (ожидается 9)");
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

                        newCard.Hero = lineParts[1];
                        newCard.Slot1 = lineParts[2];
                        newCard.Slot2 = lineParts[3];
                        newCard.Slot3 = lineParts[4];
                        newCard.Slot4 = lineParts[5];
                        newCard.Slot5 = lineParts[6];
                        newCard.Slot6 = lineParts[7];
                        newCard.Neutral = lineParts[8];

                        records.Add(newCard);
                    }

                    var nextId = records.Count == 0 ? 1 : records.Max(r => r.Id) + 1;
                    collection.ReplaceAll(records, nextId, heroes, items, neutrals);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine($"@heroes;{string.Join(";", collection.GetHeroes())}");
                    writer.WriteLine($"@items;{string.Join(";", collection.GetItems())}");
                    writer.WriteLine($"@neutrals;{string.Join(";", collection.GetNeutrals())}");

                    foreach (var item in collection.GetAll())
                    {
                        writer.WriteLine($"{item.Id};{item.Hero};{item.Slot1};{item.Slot2};{item.Slot3};{item.Slot4};{item.Slot5};{item.Slot6};{item.Neutral}");
                    }
                }
            }
        }

        private static IEnumerable<string> ParseCatalogLine(string line, string prefix)
        {
            if (line.Length <= prefix.Length)
            {
                return Enumerable.Empty<string>();
            }

            var raw = line.Substring(prefix.Length);
            return raw.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x));
        }
    }
}
