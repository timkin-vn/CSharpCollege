using Newtonsoft.Json;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Industry { get; set; }

        public int FoundationYear { get; set; }

        public decimal Revenue { get; set; }

        public int Employees { get; set; }

        public string Description { get; set; }
    }
}