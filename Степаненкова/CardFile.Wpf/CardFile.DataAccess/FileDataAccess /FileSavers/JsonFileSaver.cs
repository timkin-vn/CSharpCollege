using CardFile.Business.Services.FileSavers;
using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.Entites;
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
        public void OpenFile(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    using (var jsonReader = new JsonTextReader(reader))
                    {
                        var serializer = new JsonSerializer();
                        var jsonCollection = serializer.Deserialize<JsonCardCollection>(jsonReader);

                        collection.ReplaceAll(Mapping.Mapper.Map<List<CardDto>>(jsonCollection.Cards), jsonCollection.CurrentId);
                    }
                }
            }
        }

        public void SaveFile(string fileName, CardCollection collection)
        {
            var jsonCollection = new JsonCardCollection { CurrentId = collection.CurrentId, };
            jsonCollection.Cards.AddRange(Mapping.Mapper.Map<List<JsonCard>>(collection.GetAll()));

            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(writer, jsonCollection);
                }
            }
        }
    }
}