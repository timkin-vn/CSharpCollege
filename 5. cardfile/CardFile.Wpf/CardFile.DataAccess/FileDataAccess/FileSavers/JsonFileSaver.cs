using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.Entites;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class JsonFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                var jsonCollection = serializer.Deserialize<JsonCardCollection>(jsonReader);

                var mappedCards = Mapping.Mapper.Map<List<CardDto>>(jsonCollection.Cards);
                collection.ReplaceAll(mappedCards);
                collection.CurrentId = jsonCollection.CurrentId;
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            var jsonCollection = new JsonCardCollection
            {
                CurrentId = collection.CurrentId,
                Cards = Mapping.Mapper.Map<List<JsonCard>>(collection.GetAll().ToList())
            };

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (var writer = new StreamWriter(fs))
            {
                var serializer = new JsonSerializer { Formatting = Formatting.Indented };
                serializer.Serialize(writer, jsonCollection);
            }
        }
    }
}