using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimerList.DataStore.FileAccess.Entities
{
    public class JsonLimerCollection
    {
        public int NextId { get; set; }
        public List<JsonLimer> Limers { get; set; } = new List<JsonLimer>();
    }
}
