using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers;
internal class TextFileSaver : IFileSaver {
    public void OpenFile(string fileName, CardCollection collection) {
        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
            using (var reader = new StreamReader(fs)) {
                var records = new List<CardDto>();

                while (!reader.EndOfStream) {
                    var nextLine = reader.ReadLine();
                    if (string.IsNullOrEmpty(nextLine)) {
                        continue;
                    }

                    var lineParts = nextLine.Split(';');
                    if (lineParts.Length != 10) {
                        throw new Exception($"В строке файла {fileName} неверное количество записей");
                    }

                    var newRecord = new CardDto();
                    if (int.TryParse(lineParts[0], out var id)) {
                        newRecord.Id = id;
                    }else {
                        throw new Exception($"В строке файла {fileName} неверное значение Id");
                    }

                    newRecord.Title = lineParts[1];
                    newRecord.OriginalTitle = lineParts[2];
                    newRecord.Genre = lineParts[3];

                    if (DateTime.TryParse(lineParts[4], out var releaseDate)) {
                        newRecord.ReleaseDate = releaseDate;
                    }else {
                        throw new Exception($"В строке файла {fileName} неверное значение ReleaseDate");
                    }

                    newRecord.Studio = lineParts[5];
                    newRecord.Director = lineParts[6];

                    if (lineParts[7] == "-") {
                        newRecord.EndDate = null;
                    }else if (DateTime.TryParse(lineParts[7], out var endDate)) {
                        newRecord.EndDate = endDate;
                    }else {
                        throw new Exception($"В строке файла {fileName} неверное значение endDate");
                    }

                    if (double.TryParse(lineParts[8], out var rating)) {
                        newRecord.Rating = rating;
                    }else {
                        throw new Exception($"В строке файла {fileName} неверное значение rating");
                    }

                    records.Add(newRecord);
                }

                collection.ReplaceAll(records);
            }
        }
    }

    public void SaveFile(string fileName, CardCollection collection) {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
            using (var writer = new StreamWriter(fs)) {
                foreach (var item in collection.GetAll()) {
                    writer.WriteLine($"{item.Id};{item.Title};{item.OriginalTitle};{item.Genre};" +
                        $"{item.ReleaseDate.ToShortDateString()};{item.Studio};{item.Director};" +
                        $"{item.EndDate?.ToShortDateString() ?? "-"};" +
                        $"{item.Rating}");
                }
            }
        }
    }
}