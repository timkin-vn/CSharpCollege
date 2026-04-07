using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class JsonFileManager : IFileManager
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
                        var jsonCollection = serializer.Deserialize<JsonCardCollection>(jsonReader);

                        collection.ReplaceAll(
                            Mapping.Mapper.Map<List<CardDto>>(jsonCollection.Cards),
                            jsonCollection.NextId,
                            jsonCollection.Heroes,
                            jsonCollection.Items,
                            jsonCollection.Neutrals);
                    }
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var jsonCollection = new JsonCardCollection
            {
                NextId = collection.NextId,
                Heroes = collection.GetHeroes().ToList(),
                Items = collection.GetItems().ToList(),
                Neutrals = collection.GetNeutrals().ToList(),
                Cards = Mapping.Mapper.Map<List<JsonCard>>(collection.GetAll()),
            };
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new JsonSerializer { Formatting = Formatting.Indented, };
                    serializer.Serialize(writer, jsonCollection);
                }
            }
        }
    }
}
