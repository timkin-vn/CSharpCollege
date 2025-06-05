using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CardFile.DataAccess.FileDataAccess.Entites
{
    internal class JsonCard
    {
        public int Id { get; set; }
        public string DishName {  get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int PortionSize { get; set; }
        public double Price { get; set; }
    }
}
