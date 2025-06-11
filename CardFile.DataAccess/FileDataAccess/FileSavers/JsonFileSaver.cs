﻿using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.DataEntites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                        var jsonCollection = serializer.Deserialize<JsonCardCollection>(jsonReader);

                        collection.ReplaceAll(jsonCollection.Cards.Select(c => Mapping.Mapper.Map<CardDto>(c)), jsonCollection.NextId);
                    }
                }
            }
        }

        public void SaveToFile(string fileName, CardCollection collection)
        {
            var jsonCollection = new JsonCardCollection { NextId = collection.NextId, };
            jsonCollection.Cards.AddRange(collection.GetAll().Select(c => Mapping.Mapper.Map<JsonCard>(c)));

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
