using System.Collections.Generic;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCardCollection
    {
        public int NextId { get; set; }

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}
