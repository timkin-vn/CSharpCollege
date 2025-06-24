using GpxDataShow.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GpxDataShow.Data.Services
{
    public class DataService
    {
        public GpxData ReadFromFile(string fileName)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                using (TextReader textReader = new StreamReader(fs))
                {
                    using (XmlTextReader reader = new IgnoreNamespaceXmlTextReader(textReader))
                    {
                        var serializer = new XmlSerializer(typeof(GpxData));
                        return (GpxData)serializer.Deserialize(reader);
                    }
                }
            }
        }
    }
}
