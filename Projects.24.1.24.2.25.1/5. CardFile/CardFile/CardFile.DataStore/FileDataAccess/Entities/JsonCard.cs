using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataStore.FileDataAccess.Entities
{
    public class JsonCard
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public double GlobalRating { get; set; }
        public int MyScore { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
