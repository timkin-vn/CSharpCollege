using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class JsonFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardFileDataCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var jsonCollection = JsonSerializer.Deserialize<JsonCardCollection>(fs);
                jsonCollection.StoreToDataCollection(collection);
            }
        }

        public void SaveFile(string fileName, CardFileDataCollection collection)
        {
            var jsonCollection = new JsonCardCollection();
            jsonCollection.FillFromDataCollection(collection);

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var options = new JsonSerializerOptions();
                options.WriteIndented = true;
                options.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
                JsonSerializer.Serialize(fs, jsonCollection, options);
            }
        }
    }
}
