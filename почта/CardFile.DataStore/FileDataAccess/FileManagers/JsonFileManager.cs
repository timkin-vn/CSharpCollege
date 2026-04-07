using CardFile.Common.Infrastructure;
using CardFile.DataStore.DataCollection;
using CardFile.DataStore.Dtos;
using CardFile.DataStore.FileDataAccess.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CardFile.DataStore.FileDataAccess.FileManagers
{
    internal class JsonFileManager : IFileManager
    {
        public void OpenFromFile(string fileName, LetterCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(fs))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                var jsonCollection = serializer.Deserialize<JsonLetterCollection>(jsonReader);
                collection.ReplaceAll(Mapping.Mapper.Map<List<LetterDto>>(jsonCollection.Letters), jsonCollection.NextId);
            }
        }

        public void SaveToFile(string fileName, LetterCollection collection)
        {
            var jsonCollection = new JsonLetterCollection
            {
                NextId = collection.NextId,
                Letters = Mapping.Mapper.Map<List<JsonLetter>>(collection.GetAll()),
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