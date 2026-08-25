using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCardCollection
    {
        public int NextId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}
