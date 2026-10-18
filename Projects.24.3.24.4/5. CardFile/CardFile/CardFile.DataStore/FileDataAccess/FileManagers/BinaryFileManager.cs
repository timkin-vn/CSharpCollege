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
    public class BinaryFileManager : IFileManager
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
                            newCard.FirstName = reader.ReadString();
                            newCard.MiddleName = reader.ReadString();
                            newCard.LastName = reader.ReadString();

                            newCard.BirthDate = new DateTime(reader.ReadInt64());

                            newCard.Faculty = reader.ReadString();
                            newCard.GroupNumber = reader.ReadString();
                            newCard.RecordBookNumber = reader.ReadInt32();
                            newCard.AverageScore = reader.ReadDouble();
                            newCard.IsBudget = reader.ReadBoolean();

                            var isExpelled = reader.ReadBoolean();

                            if (isExpelled)
                            {
                                newCard.ExpulsionDate = new DateTime(reader.ReadInt64());
                            }
                            else
                            {
                                newCard.ExpulsionDate = null;
                            }

                            var hasPhoto = reader.ReadBoolean();

                            newCard.PhotoPath = hasPhoto ? reader.ReadString() : null;

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
                    foreach (var card in collection.GetAll())
                    {
                        writer.Write(card.Faculty ?? string.Empty);
                        writer.Write(card.GroupNumber ?? string.Empty);
                        writer.Write(card.RecordBookNumber);
                        writer.Write(card.AverageScore);
                        writer.Write(card.IsBudget);

                        if (card.ExpulsionDate.HasValue)
                        {
                            writer.Write(true);
                            writer.Write(card.ExpulsionDate.Value.Ticks);
                        }

                        else
                        {
                            writer.Write(false);
                        }

                        if (!string.IsNullOrEmpty(card.PhotoPath))
                        {
                            writer.Write(true);
                            writer.Write(card.PhotoPath);
                        }

                        else
                        {
                            writer.Write(false);
                        }
                    }
                }
            }
        }
    }
}
