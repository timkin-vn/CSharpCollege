using System.IO;
using System.Xml;

namespace GpxDataShow.Data.Services
{
    internal class IgnoreNamespaceXmlTextReader : XmlTextReader
    {
        public IgnoreNamespaceXmlTextReader(TextReader reader) : base(reader)
        {
        }

        public override string NamespaceURI => "";
    }
}
