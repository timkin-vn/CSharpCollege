using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.FileAccess.Entities;
using Newtonsoft.Json;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public class JsonFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                using(var sr = new StreamReader(fs))
                {
                    using(var jsonReader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer();
                        var jsonCollection = serializer.Deserialize<JsonLimerCollection>(jsonReader);
                        collection.ReplaceAll(Mapping.Mapper.Map<List<LimerDto>>(jsonCollection.Limers), jsonCollection.NextId);
                    }
                }
            }
        }

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            var jsonCollection = new JsonLimerCollection()
            {
                NextId = collection.NextId,
                Limers = Mapping.Mapper.Map<List<JsonLimer>>(collection.GetAll()),
            };
            using(var fs = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                using(var writer = new StreamWriter(fs))
                {
                    var serializer = new JsonSerializer
                    {
                        Formatting = Formatting.Indented,
                    };
                    serializer.Serialize(writer, jsonCollection);
                }
            }
        }
    }
}
