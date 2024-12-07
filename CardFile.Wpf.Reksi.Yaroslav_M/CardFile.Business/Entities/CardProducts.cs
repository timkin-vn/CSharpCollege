
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardFile.Business.Entities
{
    public class CardProducts
    {
        public int Id { get; set; }

        public string NameProducts { get; set; }

        public string TypeProducts { get; set; }

        public DateTime DateManufacture { get; set; }

        public DateTime DateExpiration { get; set; }

        public int CountProducts { get; set; }

        public decimal PriceOneProducts { get; set; }

        public string SectionProducts { get; set; }

        public string ShirtProducts { get; set; }
    }
}
