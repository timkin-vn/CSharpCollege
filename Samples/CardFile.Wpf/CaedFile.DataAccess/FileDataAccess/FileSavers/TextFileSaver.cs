using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                    var encodedData = reader.ReadToEnd();
                    var decodedData = DecodeBase64(encodedData);
                    using (var decodedReader = new StringReader(decodedData))
                    {
                        string inputLine;
                        while ((inputLine = decodedReader.ReadLine()) != null)
                        {
                            if (string.IsNullOrEmpty(inputLine))
                            {
                                continue;
                            }

                            var lineSegments = inputLine.Split(';');
                            if (lineSegments.Length != 5)
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

                            newRecord.FullName = lineSegments[1];
                            newRecord.CardNumber = lineSegments[2];

                            if (int.TryParse(lineSegments[3], out var bonusPoints))
                            {
                                newRecord.BonusPoints = bonusPoints;
                            }
                            else
                            {
                                throw new Exception($"Неверный формат файла {fileName}");
                            }

                            newRecord.CardType = lineSegments[4];

                            records.Add(newRecord);
                        }
                    }
                }
            }

            collection.ReplaceCollection(records);
        }

        public void Save(string fileName, CardCollection collection)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in collection.GetAll())
            {
                stringBuilder.AppendLine($"{item.Id};{item.FullName};{item.CardNumber};{item.BonusPoints};{item.CardType}");
            }

            var encodedData = EncodeBase64(stringBuilder.ToString());

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(encodedData);
                }
            }
        }

        private string EncodeBase64(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        private string DecodeBase64(string encodedData)
        {
            var bytes = Convert.FromBase64String(encodedData);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}