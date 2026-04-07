using System.Collections.Generic;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCardCollection
    {
        public int NextId { get; set; }

        public List<string> Heroes { get; set; } = new List<string>();

        public List<string> Items { get; set; } = new List<string>();

        public List<string> Neutrals { get; set; } = new List<string>();

        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}
