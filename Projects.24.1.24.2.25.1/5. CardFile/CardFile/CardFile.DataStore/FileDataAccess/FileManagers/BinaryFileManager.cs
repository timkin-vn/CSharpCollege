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
    internal class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(fs))
                {
                    var records = new List<CardDto>();

                    try
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var newCard = new CardDto();

                            newCard.Id = reader.ReadInt32();
                            newCard.Name = reader.ReadString();
                            newCard.LoreName = reader.ReadString();
                            newCard.PrimaryAttribute = reader.ReadString();
                            newCard.AttackType = reader.ReadString();
                            newCard.Role = reader.ReadString();

                            var ticks = reader.ReadInt64();
                            newCard.ReleaseDate = new DateTime(ticks);

                            var isReworkDatePresent = reader.ReadBoolean();
                            if (isReworkDatePresent)
                            {
                                ticks = reader.ReadInt64();
                                newCard.ReworkDate = new DateTime(ticks);
                            }
                            else
                            {
                                newCard.ReworkDate = null;
                            }

                            newCard.WinRate = reader.ReadDecimal();

                            records.Add(newCard);
                        }
                    }
                    catch
                    {
                        throw new Exception($"Неверные данные в файле {fileName}");
                    }

                    collection.ReplaceAll(records);
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new BinaryWriter(fs))
                {
                    foreach (var item in collection.GetAll())
                    {
                        writer.Write(item.Id);
                        writer.Write(item.Name ?? string.Empty);
                        writer.Write(item.LoreName ?? string.Empty);
                        writer.Write(item.PrimaryAttribute ?? string.Empty);
                        writer.Write(item.AttackType ?? string.Empty);
                        writer.Write(item.Role ?? string.Empty);
                        writer.Write(item.ReleaseDate.Ticks);

                        writer.Write(item.ReworkDate.HasValue);
                        if (item.ReworkDate.HasValue)
                        {
                            writer.Write(item.ReworkDate.Value.Ticks);
                        }

                        writer.Write(item.WinRate);
                    }
                }
            }    
        }
    }
}
