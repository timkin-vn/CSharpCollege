using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CardFile.DataAccess.FileDataAccess.StorageEntities
{
    [Serializable]
    public class JsonCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string City { get; set; }
        public int MailIndex { get; set; }
        public double Rating { get; set; }
        public int CounterReviews { get; set; }
        public string Status { get; set; }
    }
}
