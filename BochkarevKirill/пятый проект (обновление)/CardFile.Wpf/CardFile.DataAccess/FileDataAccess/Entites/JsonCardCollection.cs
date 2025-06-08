using System.Collections.Generic;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCardCollection
    {
        public int CurrentId { get; set; }
        public List<JsonCard> Cards { get; set; } = new List<JsonCard>();
    }
}