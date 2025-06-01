using CardFile.Common.Infrastructure;
using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.FileDataAccess.Entites;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class JsonFileSaver : IFileSaver
    {
        public void OpenFile(string fileName, CardCollection collection)
        {
            throw new NotImplementedException();
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
