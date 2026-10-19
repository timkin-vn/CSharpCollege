
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class BinaryFileManager : IFileManager
    {
        public void OpenFromFile(
            string fileName, CardCollection collection)
        {
            var records = new List<CardDto>();

            using (var stream = new FileStream(
                fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new BinaryReader(stream, Encoding.UTF8))
            {
                try
                {
                    while (reader.BaseStream.Position <
                           reader.BaseStream.Length)
                    {
                        var car = new CardDto
                        {
                            Id = reader.ReadInt32(),
                            Brand = reader.ReadString(),
                            Model = reader.ReadString(),
                            Year = reader.ReadInt32(),
                            Color = reader.ReadString(),
                            Mileage = reader.ReadInt32(),
                            Price = reader.ReadDecimal(),
                            FuelType = reader.ReadString(),
                            EngineVolume = reader.ReadDecimal(),
                            TransmissionType = reader.ReadString(),
                            HasAirConditioning = reader.ReadBoolean(),
                            AddedDate = DateTime.FromBinary(reader.ReadInt64())
                        };

                        bool hasSoldDate = reader.ReadBoolean();

                        car.SoldDate = hasSoldDate
                            ? DateTime.FromBinary(reader.ReadInt64())
                            : (DateTime?)null;

                        car.Vin = reader.ReadString();

                        records.Add(car);
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidDataException(
                        $"Неверные данные в бинарном файле {fileName}.", ex);
                }
            }

            collection.ReplaceAll(records);
        }

        public void SaveToFile(
            string fileName, CardCollection collection)
        {
            using (var stream = new FileStream(
                fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new BinaryWriter(stream, Encoding.UTF8))
            {
                foreach (CardDto car in collection.GetAll())
                {
                    writer.Write(car.Id);
                    writer.Write(car.Brand ?? "");
                    writer.Write(car.Model ?? "");
                    writer.Write(car.Year);
                    writer.Write(car.Color ?? "");
                    writer.Write(car.Mileage);
                    writer.Write(car.Price);
                    writer.Write(car.FuelType ?? "");
                    writer.Write(car.EngineVolume);
                    writer.Write(car.TransmissionType ?? "");
                    writer.Write(car.HasAirConditioning);
                    writer.Write(car.AddedDate.ToBinary());

                    writer.Write(car.SoldDate.HasValue);

                    if (car.SoldDate.HasValue)
                        writer.Write(car.SoldDate.Value.ToBinary());

                    writer.Write(car.Vin ?? "");
                }
            }
        }
    }
}
