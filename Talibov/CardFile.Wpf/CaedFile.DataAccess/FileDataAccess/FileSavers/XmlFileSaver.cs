using CardFile.DataAccess.DataCollection;
using CardFile.DataAccess.Dtos;
using CardFile.DataAccess.FileDataAccess.StorageEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.FileSavers
{
    internal class XmlFileSaver : IFileSaver
    {
        public void Open(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    var xmlCollection = (XmlCardCollection)serializer.Deserialize(reader);
                    xmlCollection.SaveToCollection(collection);
                }
            }
        }

        public void Save(string fileName, CardCollection collection)
        {
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                using (var writer = new StreamWriter(fs))
                {
                    var xmlCollection = new XmlCardCollection();
                    xmlCollection.FillFromCollection(collection);

                    var serializer = new XmlSerializer(typeof(XmlCardCollection));
                    serializer.Serialize(writer, xmlCollection);
                }
            }
        }
    }
}
