using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers;
internal class BinaryFileSaver : IFileSaver {
    public void OpenFile(string fileName, CardCollection collection) {
        using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
            using (var reader = new BinaryReader(fs)) {
                var records = new List<CardDto>();
                try {
                    while (reader.BaseStream.Position < reader.BaseStream.Length) {
                        var newRecord = new CardDto();

                        newRecord.Id = reader.ReadInt32();
                        newRecord.Title = reader.ReadString();
                        newRecord.OriginalTitle = reader.ReadString();
                        newRecord.Genre = reader.ReadString();

                        var ticks = reader.ReadInt64();
                        newRecord.ReleaseDate = new DateTime(ticks);

                        newRecord.Studio = reader.ReadString();
                        newRecord.Director = reader.ReadString();

                        var isDismissalDatePresent = reader.ReadBoolean();
                        if (isDismissalDatePresent) {
                            ticks = reader.ReadInt64();
                            newRecord.EndDate = new DateTime(ticks);
                        }

                        newRecord.Rating = reader.ReadDouble();

                        records.Add(newRecord);
                    }
                }catch {
                    throw new Exception($"Неверные данные в файле {fileName}");
                }

                collection.ReplaceAll(records);
            }
        }
    }

    public void SaveFile(string fileName, CardCollection collection) {
        using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write)) {
            using (var writer = new BinaryWriter(fs)) {
                foreach (var item in collection.GetAll()) {
                    writer.Write(item.Id);
                    writer.Write(item.Title);
                    writer.Write(item.OriginalTitle);
                    writer.Write(item.Genre);
                    writer.Write(item.ReleaseDate.Ticks);
                    writer.Write(item.Studio);
                    writer.Write(item.Director);
                    writer.Write(item.EndDate.HasValue);

                    if (item.EndDate.HasValue) {
                        writer.Write(item.EndDate.Value.Ticks);
                    }

                    writer.Write(item.Rating);
                }
            }
        }
    }
}