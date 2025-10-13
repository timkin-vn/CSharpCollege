using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class TextFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();

                while (!reader.EndOfStream)
                {
                    var nextLine = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(nextLine))
                        continue;

                    var lineParts = nextLine.Split(';');
                    if (lineParts.Length != 10)
                        throw new Exception($"В строке файла {fileName} неверное количество записей");

                    var newRecord = new CardDto();

                    if (!int.TryParse(lineParts[0], out var id))
                        throw new Exception($"Неверный Id в строке файла {fileName}");
                    newRecord.Id = id;

                    newRecord.FirstName = lineParts[1];
                    newRecord.MiddleName = lineParts[2];
                    newRecord.LastName = lineParts[3];

                    if (!DateTime.TryParse(lineParts[4], out var birthDate))
                        throw new Exception($"Неверный BirthDate в строке файла {fileName}");
                    newRecord.BirthDate = birthDate;

                    newRecord.Gender = lineParts[5];
                    newRecord.Address = lineParts[6];
                    newRecord.Diagnosis = lineParts[7];

                    if (lineParts[8] == "-")
                        newRecord.LastVisitDate = null;
                    else if (DateTime.TryParse(lineParts[8], out var visitDate))
                        newRecord.LastVisitDate = visitDate;
                    else
                        throw new Exception($"Неверный LastVisitDate в строке файла {fileName}");

                    newRecord.PhoneNumber = lineParts[9];

                    records.Add(newRecord);
                }

                collection.ReplaceAll(records);
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                foreach (var item in collection.GetAll())
                {
                    writer.WriteLine($"{item.Id};" +
    $"{item.FirstName};" +
    $"{item.MiddleName};" +
    $"{item.LastName};" +
    $"{item.BirthDate:yyyy-MM-dd};" +
    $"{(string.IsNullOrEmpty(item.Gender) ? "-" : item.Gender)};" +
    $"{(string.IsNullOrEmpty(item.Address) ? "-" : item.Address)};" +
    $"{(string.IsNullOrEmpty(item.Diagnosis) ? "-" : item.Diagnosis)};" +
    $"{(item.LastVisitDate.HasValue ? item.LastVisitDate.Value.ToString("yyyy-MM-dd") : "-")};" +
    $"{(string.IsNullOrEmpty(item.PhoneNumber) ? "-" : item.PhoneNumber)};" +
    $"{(string.IsNullOrEmpty(item.InsuranceNumber) ? "-" : item.InsuranceNumber)}");
                }
            }
        }
    }
}