using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            using (var reader = new StreamReader(fs))
            {
                var records = new List<CardDto>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line)) continue;

                    // Восстановление экранированных символов
                    string processedLine = line.Replace("\\;", "\u0001");
                    var lineParts = processedLine.Split(';');
                    for (int i = 0; i < lineParts.Length; i++)
                    {
                        lineParts[i] = lineParts[i].Replace("\u0001", ";").Replace("\\\\", "\\");
                    }

                    if (lineParts.Length != 10)
                        throw new Exception($"В строке файла {fileName} неверное количество полей");

                    var newCard = new CardDto();

                    if (!int.TryParse(lineParts[0], out var id))
                        throw new Exception($"В строке файла {fileName} неверное значение Id");
                    newCard.Id = id;

                    newCard.ProductName = Unescape(lineParts[1]);
                    newCard.ProductModel = Unescape(lineParts[2]);
                    newCard.ProductColor = Unescape(lineParts[3]);

                    if (!DateTime.TryParse(lineParts[4], out var manufactureDate))
                        throw new Exception($"В строке файла {fileName} неверное значение ManufactureDate");
                    newCard.ManufactureDate = manufactureDate;

                    newCard.Category = Unescape(lineParts[5]);
                    newCard.Manufacturer = Unescape(lineParts[6]);

                    if (!DateTime.TryParse(lineParts[7], out var receiptDate))
                        throw new Exception($"В строке файла {fileName} неверное значение ReceiptDate");
                    newCard.ReceiptDate = receiptDate;

                    if (lineParts[8] == "-")
                        newCard.WriteOffDate = null;
                    else if (DateTime.TryParse(lineParts[8], out var writeOffDate))
                        newCard.WriteOffDate = writeOffDate;
                    else
                        throw new Exception($"В строке файла {fileName} неверное значение WriteOffDate");

                    if (!decimal.TryParse(lineParts[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var price))
                        throw new Exception($"В строке файла {fileName} неверное значение Price");
                    newCard.Price = price;

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
                    writer.WriteLine($"{item.Id};{Escape(item.ProductName)};{Escape(item.ProductModel)};{Escape(item.ProductColor)};" +
                        $"{item.ManufactureDate.ToShortDateString()};{Escape(item.Category)};{Escape(item.Manufacturer)};" +
                        $"{item.ReceiptDate.ToShortDateString()};{item.WriteOffDate?.ToShortDateString() ?? "-"};" +
                        $"{item.Price.ToString(CultureInfo.InvariantCulture)}");
                }
            }
        }

        private string Escape(string s) => s?.Replace("\\", "\\\\").Replace(";", "\\;") ?? "";
        private string Unescape(string s) => s?.Replace("\\\\", "\\").Replace("\\;", ";") ?? "";
    }
}