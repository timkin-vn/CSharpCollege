using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    public class TextFileManager : IFileManager
    {
        private const string DateFormat = "yyyy-MM-dd";

        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
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
                    if (lineParts.Length != 10)
                    {
                        throw new Exception($"В строке файла {fileName} неверное количество полей");
                    }

                    var newCard = new CardDto();

                    if (int.TryParse(lineParts[0], out var id))
                        newCard.Id = id;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Id");

                    newCard.Brand = lineParts[1];
                    newCard.Model = lineParts[2];
                    newCard.LicensePlate = lineParts[3];

                    if (DateTime.TryParseExact(lineParts[4], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date1))
                        newCard.ManufactureDate = date1;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение ManufactureDate");

                    newCard.Color = lineParts[5];
                    newCard.Owner = lineParts[6];

                    if (DateTime.TryParseExact(lineParts[7], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date2))
                        newCard.RegistrationDate = date2;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение RegistrationDate");

                    if (lineParts[8] == "-")
                        newCard.DeregistrationDate = null;
                    else if (DateTime.TryParseExact(lineParts[8], DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date3))
                        newCard.DeregistrationDate = date3;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение DeregistrationDate");

                    if (decimal.TryParse(lineParts[9], NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
                        newCard.Price = amount;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение Price");

                    records.Add(newCard);
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id};{item.Brand};{item.Model};{item.LicensePlate};" +
                        $"{item.ManufactureDate.ToString(DateFormat, CultureInfo.InvariantCulture)};{item.Color};{item.Owner};" +
                        $"{item.RegistrationDate.ToString(DateFormat, CultureInfo.InvariantCulture)};{(item.DeregistrationDate.HasValue ? item.DeregistrationDate.Value.ToString(DateFormat, CultureInfo.InvariantCulture) : "-")};" +
                        $"{item.Price.ToString(CultureInfo.InvariantCulture)}");
                }
            }
        }
    }
}
