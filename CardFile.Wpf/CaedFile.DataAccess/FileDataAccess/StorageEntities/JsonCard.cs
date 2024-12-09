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

        public string LettClass { get; set; }

        public string NumClass { get; set; }

        public string ClassLed { get; set; }

        public string BadClass { get; set; }

        public int ChildrenCount { get; set; }
    }
}
