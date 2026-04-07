using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
    {
        private const int Magic = unchecked((int)0x41544F44);
        private const int Version = 1;

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<CardDto>();
                    var heroes = new List<string>();
                    var items = new List<string>();
                    var neutrals = new List<string>();
                    var nextId = 1;

                    try
                    {
                        if (reader.BaseStream.Length < sizeof(int))
                        {
                            throw new Exception($"Неверные данные в файле {fileName}");
                        }

                        var magic = reader.ReadInt32();
                        if (magic != Magic)
                        {
                            throw new Exception($"Файл {fileName} имеет старый/неподдерживаемый бинарный формат");
                        }

                        var version = reader.ReadInt32();
                        if (version != Version)
                        {
                            throw new Exception($"Файл {fileName} имеет неподдерживаемую версию формата");
                        }

                        nextId = reader.ReadInt32();

                        heroes.AddRange(ReadStringList(reader));
                        items.AddRange(ReadStringList(reader));
                        neutrals.AddRange(ReadStringList(reader));

                        var count = reader.ReadInt32();
                        for (var i = 0; i < count; i++)
                        {
                            var newCard = new CardDto
                            {
                                Id = reader.ReadInt32(),
                                Hero = reader.ReadString(),
                                Slot1 = reader.ReadString(),
                                Slot2 = reader.ReadString(),
                                Slot3 = reader.ReadString(),
                                Slot4 = reader.ReadString(),
                                Slot5 = reader.ReadString(),
                                Slot6 = reader.ReadString(),
                                Neutral = reader.ReadString(),
                            };

                            records.Add(newCard);
                        }
                    }
                    catch
                    {
                        throw new Exception($"Неверные данные в файле {fileName}");
                    }

                    collection.ReplaceAll(records, nextId, heroes, items, neutrals);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    writer.Write(Magic);
                    writer.Write(Version);
                    writer.Write(collection.NextId);

                    WriteStringList(writer, collection.GetHeroes());
                    WriteStringList(writer, collection.GetItems());
                    WriteStringList(writer, collection.GetNeutrals());

                    var all = collection.GetAll().ToList();
                    writer.Write(all.Count);

                    foreach (var item in all)
                    {
                        writer.Write(item.Id);
                        writer.Write(item.Hero ?? "");
                        writer.Write(item.Slot1 ?? "");
                        writer.Write(item.Slot2 ?? "");
                        writer.Write(item.Slot3 ?? "");
                        writer.Write(item.Slot4 ?? "");
                        writer.Write(item.Slot5 ?? "");
                        writer.Write(item.Slot6 ?? "");
                        writer.Write(item.Neutral ?? "");
                    }
                }
            }    
        }

        private static void WriteStringList(BinaryWriter writer, IEnumerable<string> values)
        {
            var list = (values ?? Enumerable.Empty<string>()).Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
            writer.Write(list.Count);
            foreach (var v in list)
            {
                writer.Write(v);
            }
        }

        private static IEnumerable<string> ReadStringList(BinaryReader reader)
        {
            var count = reader.ReadInt32();
            var result = new List<string>(count);
            for (var i = 0; i < count; i++)
            {
                result.Add(reader.ReadString());
            }
            return result;
        }
    }
}
