using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LimerList.Common.Infrastructure;
using LimerList.DataStore.DataCollection;
using LimerList.DataStore.Dtos;
using LimerList.DataStore.FileAccess.Entities;

namespace LimerList.DataStore.FileAccess.FileManagers
{
    public class XmlFileManager : IFileManager
    {
        public void OpenFromFile(string filePath, LimerCollection collection)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, System.IO.FileAccess.Read))
            {
                using(var reader = new StreamReader(stream))
                {
                    var serializer = new XmlSerializer(typeof(XmlLimerCollection));
                    var xmlCollection = (XmlLimerCollection)serializer.Deserialize(reader);

                    collection.ReplaceAll(Mapping.Mapper.Map<List<LimerDto>>(xmlCollection.Limers), xmlCollection.NextId);
                }
            }
        }

        public void SaveToFile(string filePath, LimerCollection collection)
        {
            var xmlCollection = new XmlLimerCollection()
            {
                NextId = collection.NextId,
                Limers = Mapping.Mapper.Map<List<XmlLimer>>(collection.GetAll())
            };
            using (var fs = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                using(var writer = new StreamWriter(fs))
                {
                    var serilaizer = new XmlSerializer(typeof(XmlLimerCollection));
                    serilaizer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}
