using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class JsonFileSaver : IFileSaver
    {
        public void OpenFromFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        var serializer = new JsonSerializer();
                        var cards = serializer.Deserialize<List<CardDto>>(jsonReader);
                        collection.ReplaceAll(cards);
                    }
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var cards = new List<CardDto>(collection.GetAll());

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new JsonSerializer();
                    serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
                    serializer.Serialize(writer, cards);
                }
            }
        }
    }
}