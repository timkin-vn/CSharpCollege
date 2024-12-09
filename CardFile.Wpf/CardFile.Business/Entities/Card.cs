using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Business.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public string LettClass { get; set; }

        public string NumClass { get; set; }

        public string ClassLed { get; set; }

        public string BadClass { get; set; }
        public int ChildrenCount { get; set; }
    }
}
