using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.StorageEntities;
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
        public void Open(string fileName, CardProductsCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var jsonCollection = JsonSerializer.Deserialize<JsonCardCollection>(fs);
                jsonCollection.SaveToCollection(collection);
            }
        }

        public void Save(string fileName, CardProductsCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                var jsonCollection = new JsonCardCollection();
                jsonCollection.FillFromCollection(collection);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                };

                JsonSerializer.Serialize(fs, jsonCollection, options);
            }
        }
    }
}
