using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCardCollection
    {
        public int CurrentId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}