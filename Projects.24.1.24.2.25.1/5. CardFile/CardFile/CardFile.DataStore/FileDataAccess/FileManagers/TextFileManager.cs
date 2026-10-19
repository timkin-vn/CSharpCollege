
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
        private static readonly CultureInfo Culture =
            CultureInfo.InvariantCulture;

        public void OpenFromFile(
            string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var reader = new StreamReader(fileName))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(';');

                    if (parts.Length != 14)
                    {
                        throw new InvalidDataException(
                            $"В строке {lineNumber} файла {fileName} " +
                            "неверное количество полей.");
                    }

                    try
                    {
                        records.Add(new CardDto
                        {
                            Id = int.Parse(parts[0], Culture),
                            Brand = parts[1],
                            Model = parts[2],
                            Year = int.Parse(parts[3], Culture),
                            Color = parts[4],
                            Mileage = int.Parse(parts[5], Culture),
                            Price = decimal.Parse(parts[6], Culture),
                            FuelType = parts[7],
                            EngineVolume = decimal.Parse(parts[8], Culture),
                            TransmissionType = parts[9],
                            HasAirConditioning = bool.Parse(parts[10]),
                            AddedDate = DateTime.Parse(
                                parts[11], Culture,
                                DateTimeStyles.RoundtripKind),
                            SoldDate = parts[12] == "-"
                                ? (DateTime?)null
                                : DateTime.Parse(
                                    parts[12], Culture,
                                    DateTimeStyles.RoundtripKind),
                            Vin = parts[13]
                        });
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidDataException(
                            $"Ошибка чтения строки {lineNumber} файла " +
                            $"{fileName}: {ex.Message}", ex);
                    }
                }
            }

            collection.ReplaceAll(records);
        }

        public void SaveToFile(
            string fileName, CardCollection collection)
        {
            using (var writer = new StreamWriter(fileName, false))
            {
                foreach (CardDto car in collection.GetAll())
                {
                    writer.WriteLine(string.Join(";", new[]
                    {
                        car.Id.ToString(Culture),
                        car.Brand ?? "",
                        car.Model ?? "",
                        car.Year.ToString(Culture),
                        car.Color ?? "",
                        car.Mileage.ToString(Culture),
                        car.Price.ToString(Culture),
                        car.FuelType ?? "",
                        car.EngineVolume.ToString(Culture),
                        car.TransmissionType ?? "",
                        car.HasAirConditioning.ToString(),
                        car.AddedDate.ToString("O", Culture),
                        car.SoldDate.HasValue
                            ? car.SoldDate.Value.ToString("O", Culture)
                            : "-",
                        car.Vin ?? ""
                    }));
                }
            }
        }
    }
}
