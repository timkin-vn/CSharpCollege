using System.Collections.Generic;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonLetterCollection
    {
        public int NextId { get; set; }
        public List<JsonLetter> Letters { get; set; } = new List<JsonLetter>();
    }
}