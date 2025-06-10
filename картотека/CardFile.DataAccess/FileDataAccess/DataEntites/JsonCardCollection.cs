using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CardFile.DataAccess.FileDataAccess.Entites;
namespace CardFile.DataAccess.FileDataAccess.DataEntites
{
    internal class JsonCardCollection
    {
        public int NextId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}
